using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class JournalGenerator : IJournalGenerator
    {
        IJournalPoster _journalPoster;
        ISequenceNumberGeneator _sequenceNumberGeneator;
        IContextParameterResolver _contextParameterResolver;
        public JournalGenerator(IJournalPoster journalPoster, ISequenceNumberGeneator sequenceNumberGeneator, IContextParameterResolver contextParameterResolver)
        {
            _journalPoster = journalPoster;
            _sequenceNumberGeneator = sequenceNumberGeneator;
            _contextParameterResolver = contextParameterResolver;
        }
        
        public JournalRunResult Run(IDataContext db, TransactionTrigger transactionTrigger)
        {
            JournalRunResult result = new JournalRunResult();

            if (transactionTrigger.ReportingEntityTransactionTrigger != null)
            {
                var resolvedPublic = transactionTrigger.Public;
                if (transactionTrigger.JournalTemplate.PublicRequirement != null)
                {
                    if (transactionTrigger.JournalTemplate.PublicRequirement.IsServiceProvider)
                        resolvedPublic = transactionTrigger.ServiceProvider.Public;

                    if (transactionTrigger.JournalTemplate.PublicRequirement.IsAgent)
                        resolvedPublic = transactionTrigger.Agent.Public;
                }

                Journal journal = BuildJournal(db, transactionTrigger, resolvedPublic, transactionTrigger.ServiceProvider, transactionTrigger.Agent, 1);
                _journalPoster.Run(db, journal);
                result.Journals.Add(journal);
            }


            if (transactionTrigger.PolicyTransactionTrigger != null)
            {
                var policyTransactionTrigger = (transactionTrigger.PolicyTransactionTrigger);
                if (transactionTrigger.JournalTemplate.PublicRequirement != null)
                {
                    if (transactionTrigger.JournalTemplate.PublicRequirement.IsPolicyHolder)
                    {
                        decimal totalPerc = 0;
                        foreach (var policyHolder in policyTransactionTrigger.Policy.PolicyHolders)
                        {
                            if (policyHolder.BillingPercent.HasValue)
                            {
                                if (policyHolder.BillingPercent.Value != 0)
                                {
                                    Journal journal = BuildJournal(db, transactionTrigger, policyHolder.Public, null, null, policyHolder.BillingPercent.Value);
                                    _journalPoster.Run(db, journal);
                                    result.Journals.Add(journal);
                                }
                                totalPerc += policyHolder.BillingPercent.Value;
                            }                            
                        }
                        if (totalPerc != 1)
                            throw new Exception("Policy holder split does not add up to 100%");
                    }

                    if (transactionTrigger.JournalTemplate.PublicRequirement.IsServiceProvider)
                    {
                        decimal totalPerc = 0;
                        foreach (var sp in policyTransactionTrigger.Policy.ServiceProviders.Where(x => x.ServiceProviderTypeID == transactionTrigger.JournalTemplate.ServiceProviderTypeID))
                        {                            
                            if (sp.Percentage != 0)
                            {
                                Journal journal = BuildJournal(db, transactionTrigger, sp.ServiceProvider.Public, sp.ServiceProvider, null, sp.Percentage);
                                _journalPoster.Run(db, journal);
                                result.Journals.Add(journal);
                            }
                            totalPerc += sp.Percentage;
                            
                        }
                        if (totalPerc != 1)
                            throw new Exception("Service Provider split does not add up to 100%");
                    }

                    if (transactionTrigger.JournalTemplate.PublicRequirement.IsAgent)
                    {
                        decimal totalPerc = 0;
                        foreach (var agent in policyTransactionTrigger.Policy.Agents.Where(x => x.AgentTypeID == transactionTrigger.JournalTemplate.AgentTypeID))
                        {
                            if (agent.Percentage != 0)
                            {
                                Journal journal = BuildJournal(db, transactionTrigger, agent.Agent.Public, null, agent.Agent, agent.Percentage);
                                _journalPoster.Run(db, journal);
                                result.Journals.Add(journal);
                            }
                            totalPerc += agent.Percentage;

                        }
                        if (totalPerc != 1)
                            throw new Exception("Agent split does not add up to 100%");
                    }

                }
                else
                {
                    Journal journal = BuildJournal(db, transactionTrigger, transactionTrigger.Public, transactionTrigger.ServiceProvider, transactionTrigger.Agent, 1);
                    _journalPoster.Run(db, journal);
                    result.Journals.Add(journal);
                }
            }

            IncrementTrigger(db, transactionTrigger);
            
            return result;
        }

        private Journal BuildJournal(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, ServiceProvider serviceProvider, Agent agent, decimal percentage)
        {            
            Journal journal = new Journal();
            journal.Description = transactionTrigger.Description;
            journal.JournalType = transactionTrigger.JournalTemplate.JournalType;
            journal.ReportingEntity = transactionTrigger.ReportingEntity;
            //journal.TransactionOrigin = transactionTrigger.TransactionOrigin;

            if (transactionTrigger.PolicyTransactionTrigger != null)
                journal.Policy = transactionTrigger.PolicyTransactionTrigger.Policy;

            journal.Public = resolvedPublic;
            journal.ServiceProvider = serviceProvider;
            journal.Agent = agent;
            journal.TxnDate = transactionTrigger.TxnDate.Value.Date;
            journal.Reference = ResolveReference(db, transactionTrigger);

            db.Journals.Add(journal);

            foreach (var templateTxn in transactionTrigger.JournalTemplate.JournalTemplateTxns)
            {
                if (templateTxn.JournalTxnClass.OfCoveragePremium)
                {
                    BuildCoverageTxns(db, transactionTrigger, resolvedPublic, journal, templateTxn, percentage);                    
                }
                else
                {
                    BuildStandardTxn(db, transactionTrigger, resolvedPublic, journal, templateTxn, percentage);
                }
            }

            return journal;
        }

        private void BuildStandardTxn(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, Journal journal, JournalTemplateTxn templateTxn, decimal percentage)
        {
            JournalTxn txn = new JournalTxn();

            if (templateTxn.JournalTxnClass.IsDefinedAmount)
                txn.Amount = (ResolveAmount(db, transactionTrigger, resolvedPublic, templateTxn) * percentage);

            if (templateTxn.JournalTxnClass.OfLedgerAccount)
                txn.Amount = ((ResolveAmount(db, transactionTrigger, resolvedPublic, templateTxn) * ResolveLedgerBalance(db, transactionTrigger, templateTxn, resolvedPublic)) * percentage);

            txn.Description = templateTxn.Description;
            if (transactionTrigger.PolicyTransactionTrigger != null)
                journal.Policy = transactionTrigger.PolicyTransactionTrigger.Policy;

            txn.Public = resolvedPublic;
            txn.ReportingEntityBranch = transactionTrigger.ReportingEntityBranch;
            //txn.TransactionOrigin = transactionTrigger.TransactionOrigin;
            txn.TxnDate = transactionTrigger.TxnDate.Value.Date;
            txn.JournalTemplateTxn = templateTxn;

            journal.JournalTxns.Add(txn);
        }
        
        private void BuildCoverageTxns(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, Journal journal, JournalTemplateTxn templateTxn, decimal percentage)
        {
            if (transactionTrigger.PolicyTransactionTrigger != null)
            {
                var policyTransactionTrigger = (transactionTrigger.PolicyTransactionTrigger);
                foreach (var coverage in policyTransactionTrigger.Policy.Coverages)
                {
                    if (!coverage.Premium.HasValue)
                        throw new Exception("Coverage premium not set");

                    JournalTxn txn = new JournalTxn();
                    txn.Amount = ((ResolveAmount(db, transactionTrigger, resolvedPublic, templateTxn) * coverage.Premium.Value) * percentage);
                    txn.Description = templateTxn.Description + " - " + coverage.CoverageType.Name;
                    txn.Policy = policyTransactionTrigger.Policy;
                    txn.Public = resolvedPublic;
                    //sp ??
                    txn.ReportingEntityBranch = transactionTrigger.ReportingEntityBranch;
                    //txn.TransactionOrigin = transactionTrigger.TransactionOrigin;
                    txn.TxnDate = transactionTrigger.TxnDate.Value.Date;
                    txn.JournalTemplateTxn = templateTxn;
                    txn.PolicyCoverage = coverage;

                    journal.JournalTxns.Add(txn);
                }
            }  
        }

        private decimal ResolveAmount(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, JournalTemplateTxn templateTxn)
        {
            if (templateTxn.Amount.HasValue)
            {
                return templateTxn.Amount.Value;
            }

            if (templateTxn.AmountInputID.HasValue)
            {
                var amountInput = transactionTrigger.Inputs.First(x => x.JournalTemplateInputID == templateTxn.AmountInputID);
                return Convert.ToDecimal(amountInput.Value);
            }

            if (templateTxn.AmountContextParameterID.HasValue)
            {
                return ResolveContextParameter(db, transactionTrigger, templateTxn, resolvedPublic);
            }

            throw new Exception("Unable to resolve amount");
        }

        private string ResolveReference(IDataContext db, TransactionTrigger transactionTrigger)
        {
            if (transactionTrigger.JournalTemplate.ReferenceInputID.HasValue)
            {
                var refInput = transactionTrigger.Inputs.First(x => x.JournalTemplateInputID == transactionTrigger.JournalTemplate.ReferenceInputID);
                return refInput.Value;
            }

            if (transactionTrigger.JournalTemplate.SequenceNumberID.HasValue)
            {
                return _sequenceNumberGeneator.GetNextNumber(db, transactionTrigger.JournalTemplate.SequenceNumberID.Value);
            }

            return null;
        }

        private decimal ResolveLedgerBalance(IDataContext db, TransactionTrigger transactionTrigger, JournalTemplateTxn templateTxn, Public resolvedPublic)
        {
            if (templateTxn.AmountLedgerAccount == null)
                throw new Exception("Ledger account not specified");

            PolicyTransactionTrigger policyTransactionTrigger = null;
            if (transactionTrigger.PolicyTransactionTrigger != null)
                policyTransactionTrigger = (transactionTrigger.PolicyTransactionTrigger);

                var query = db.LedgerTxns.Where(x => x.ReportingEntityID == transactionTrigger.ReportingEntityID && x.LedgerAccountID == templateTxn.AmountLedgerAccountID && x.TxnDate <= transactionTrigger.TxnDate);

            switch (templateTxn.BalanceOrigin)
            {
                case "P":
                    query = query.Where(x => x.PolicyID == policyTransactionTrigger.PolicyID);
                    break;
                case "U":
                    if (resolvedPublic == null)
                        throw new Exception("No public to resolve ledger balance for");

                    query = query.Where(x => x.PublicID == resolvedPublic.ID);
                    break;
                case "X":
                    if (resolvedPublic == null)
                        throw new Exception("No public to resolve ledger balance for");

                    query = query.Where(x => x.PublicID == resolvedPublic.ID && x.PolicyID == policyTransactionTrigger.PolicyID);
                    break;
                    //todo: agents, sps, etc...
            }

            return query.Sum(x => x.Amount);
        }

        private decimal ResolveContextParameter(IDataContext db, TransactionTrigger trigger, JournalTemplateTxn templateTxn, Public resolvedPublic)
        {
            if (trigger.PolicyTransactionTrigger != null)
            {
                var policyTransactionTrigger = (trigger.PolicyTransactionTrigger);
                if (policyTransactionTrigger.Policy != null)
                {
                    return _contextParameterResolver.Resolve(db, trigger.TxnDate.Value, templateTxn.AmountContextParameterID.Value, policyTransactionTrigger.Policy);
                }
            }

            throw new NotImplementedException();
        }

        private void IncrementTrigger(IDataContext db, TransactionTrigger transactionTrigger)
        {

            switch (transactionTrigger.TransactionTriggerFrequency.Code)
            {
                case "O":
                    transactionTrigger.Status = db.TransactionTriggerStatuses.First(x => x.Code == "S");
                    break;
                case "D":
                    transactionTrigger.NextExecutionDate = transactionTrigger.NextExecutionDate.Value.AddDays(1);
                    transactionTrigger.TxnDate = transactionTrigger.TxnDate.Value.AddDays(1);
                    break;
                case "BW":
                    transactionTrigger.NextExecutionDate = transactionTrigger.NextExecutionDate.Value.AddDays(14);
                    transactionTrigger.TxnDate = transactionTrigger.TxnDate.Value.AddDays(14);
                    break;
                case "M":
                    transactionTrigger.NextExecutionDate = transactionTrigger.NextExecutionDate.Value.AddMonths(1);
                    transactionTrigger.TxnDate = transactionTrigger.TxnDate.Value.AddMonths(1);
                    break;
                case "A":
                    transactionTrigger.NextExecutionDate = transactionTrigger.NextExecutionDate.Value.AddYears(1);
                    transactionTrigger.TxnDate = transactionTrigger.TxnDate.Value.AddYears(1);
                    break;
            }
            
            if (transactionTrigger.EffectiveTo.HasValue)
            {
                if (transactionTrigger.EffectiveTo.Value < DateTime.Now)
                {
                    transactionTrigger.Status = db.TransactionTriggerStatuses.First(x => x.Code == "S");
                }
            }
            
        }
        
    }
        
}
