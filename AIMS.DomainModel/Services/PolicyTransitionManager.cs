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


        public PolicyTransitionResponse Transition(Policy policy, PolicyTransitionRequest request, IDataContext db)
        {
            PolicyTransitionResponse response = new PolicyTransitionResponse();
            try
            {
                var transition = db.PolicyTypeTransitions.Find(request.PolicyTypeTransitionID);

                foreach (var journal in transition.JournalTemplates)
                {
                    var targetEntities = policy.ReportingEntities.Where(x => x.PolicyTypeEntityRequirementID == journal.EntityRequirementID).ToList();

                    foreach (var entity in targetEntities)
                    {
                        if (!String.IsNullOrEmpty(journal.Condition))
                        {
                            Dictionary<string, object> exprParameters = new Dictionary<string, object>();
                            exprParameters.Add("policy", policy);
                            exprParameters.Add("request", request);
                            if (!_scriptEngine.ResolveExpression<bool>(exprParameters, db, journal.Condition, "Python"))
                                continue;
                        }

                        TransactionTrigger txnTrigger = new TransactionTrigger();
                        PolicyTransactionTrigger ptxTrigger = new PolicyTransactionTrigger();
                        ptxTrigger.TransactionTrigger = txnTrigger;
                        policy.TransactionTriggers.Add(ptxTrigger);

                        txnTrigger.ReportingEntity = entity.ReportingEntity;                        
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
                            txnTrigger.Inputs.Add(txInput);
                            txInput.JournalTemplateInputID = input.JournalTemplateInputID;                            

                            if (input.PolicyTypeTransitionInputID.HasValue)
                            {
                                var strValue = request.Inputs.First(x => x.PolicyTypeTransitionInputID == input.PolicyTypeTransitionInputID.Value).Value;
                                txInput.Value = strValue;
                            }

                            if (!String.IsNullOrEmpty(input.Expression))
                            {
                                var exprResult = _scriptEngine.ResolveExpression<TransactionTriggerInput>(txInput, "input", db, input.Expression, "Python");
                                txInput.Value = Convert.ToString(exprResult);
                            }                            
                        }                                                                       
                    }                    
                }

                if (!String.IsNullOrEmpty(transition.Script))
                {
                    Dictionary<string, object> scriptParameters = new Dictionary<string, object>();
                    scriptParameters.Add("policy", policy);
                    scriptParameters.Add("request", request);
                    var scriptResult = _scriptEngine.Run(scriptParameters, db, transition.Script, "Python");
                    if (!scriptResult.Success)
                    {
                        string errMsg = string.Empty;
                        foreach (var s in scriptResult.Log)
                            errMsg += s;
                        throw new Exception(errMsg);
                    }
                }

                policy.Status = transition.TargetStatus;
                response.Success = true;
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
