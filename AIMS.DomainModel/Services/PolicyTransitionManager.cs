using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;
using AIMS.Services.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class PolicyTransitionManager : IPolicyTransitionManager
    {
        IScriptRunner _scriptEngine;

        public PolicyTransitionManager(IScriptRunner scriptEngine)
        {
            _scriptEngine = scriptEngine;
        }


        public PolicyTransitionResponse Transition(PolicyTransitionRequest request, IDataContext db)
        {
            PolicyTransitionResponse response = new PolicyTransitionResponse();
            try
            {
                var policy = db.Policies.Find(request.PolicyID);
                var transition = db.PolicyTypeTransitions.Find(request.PolicyTypeTransitionID);


                foreach (var journal in transition.JournalTemplates)
                {
                    var targetEntities = policy.ReportingEntities.Where(x => x.PolicyTypeEntityRequirementID == journal.EntityRequirementID).ToList();

                    foreach (var entity in targetEntities)
                    {
                        //entity.ReportingEntityID
                        TransactionTrigger txnTrigger = new TransactionTrigger();
                        txnTrigger.Description = journal.JournalTemplate.Description;
                        txnTrigger.JournalTemplate = journal.JournalTemplate;
                        txnTrigger.Status = journal.TransactionTriggerStatus;
                        txnTrigger.TransactionTriggerFrequency = journal.TransactionTriggerFrequency;

                        if (journal.TxnDateInputID.HasValue)
                        {
                            var strValue = request.Inputs.First(x => x.PolicyTypeTransitionInputID == journal.TxnDateInputID.Value).Value;
                            txnTrigger.NextExecutionDate = DateTime.Parse(strValue);
                            txnTrigger.TxnDate = DateTime.Parse(strValue);
                        }

                        foreach (var input in journal.Inputs)
                        {
                            TransactionTriggerInput txInput = new TransactionTriggerInput();
                            txInput.JournalTemplateInputID = input.JournalTemplateInputID;                            

                            if (input.PolicyTypeTransitionInputID.HasValue)
                            {
                                var strValue = request.Inputs.First(x => x.PolicyTypeTransitionInputID == input.PolicyTypeTransitionInputID.Value).Value;
                                txInput.Value = strValue;
                            }

                            if (!String.IsNullOrEmpty(input.Expression))
                            {

                            }

                            txnTrigger.Inputs.Add(txInput);
                        }


                        PolicyTransactionTrigger ptxTrigger = new PolicyTransactionTrigger();
                        ptxTrigger.TransactionTrigger = txnTrigger;
                        policy.TransactionTriggers.Add(ptxTrigger);
                    }
                    
                }

                if (!String.IsNullOrEmpty(transition.Script))
                {
                    var scriptResult = _scriptEngine.Run<Policy>(policy, "policy", db, transition.Script, "Python");
                    if (!scriptResult.Success)
                    {
                        string errMsg = string.Empty;
                        foreach (var s in scriptResult.Log)
                            errMsg += s;
                        throw new Exception(errMsg);
                    }
                }

                policy.Status = transition.TargetStatus;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
