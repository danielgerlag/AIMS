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

            Journal journal = BuildJournal(db, transactionTrigger, transactionTrigger.Public, null, null); //todo
            _journalPoster.Run(db, journal);

            result.Journals.Add(journal);

            IncrementTrigger(db, transactionTrigger);
            
            return result;
        }

        private Journal BuildJournal(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, ServiceProvider serviceProvider, Agent agent)
        {
            Journal journal = new Journal();
            journal.Description = transactionTrigger.Description;
            journal.JournalType = transactionTrigger.JournalTemplate.JournalType;
            journal.ReportingEntity = transactionTrigger.ReportingEntity;
            journal.TransactionOrigin = transactionTrigger.TransactionOrigin;
            journal.Policy = transactionTrigger.Policy;
            journal.Public = resolvedPublic;
            journal.ServiceProvider = serviceProvider;
            journal.Agent = agent;
            journal.Reference = ResolveReference(db, transactionTrigger);

            db.Journals.Add(journal);

            foreach (var templateTxn in transactionTrigger.JournalTemplate.JournalTemplateTxns)
            {
                if (templateTxn.JournalTxnClass.OfCoveragePremium)
                {
                    BuildCoverageTxns(db, transactionTrigger, resolvedPublic, journal, templateTxn);                    
                }
                else
                {
                    BuildStandardTxn(db, transactionTrigger, resolvedPublic, journal, templateTxn);
                }
            }

            return journal;
        }

        private void BuildStandardTxn(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, Journal journal, JournalTemplateTxn templateTxn)
        {
            JournalTxn txn = new JournalTxn();

            if (templateTxn.JournalTxnClass.IsDefinedAmount)
                txn.Amount = ResolveAmount(db, transactionTrigger, resolvedPublic, templateTxn);

            if (templateTxn.JournalTxnClass.OfLedgerAccount)
                txn.Amount = (ResolveAmount(db, transactionTrigger, resolvedPublic, templateTxn) * ResolveLedgerBalance(db, transactionTrigger, templateTxn, resolvedPublic));

            txn.Description = templateTxn.Description;
            txn.Policy = transactionTrigger.Policy;
            txn.Public = transactionTrigger.Public;
            txn.ReportingEntityBranch = transactionTrigger.ReportingEntityBranch;
            txn.TransactionOrigin = transactionTrigger.TransactionOrigin;
            txn.TxnDate = transactionTrigger.TxnDate.Value;
            txn.JournalTemplateTxn = templateTxn;

            journal.JournalTxns.Add(txn);
        }
        
        private void BuildCoverageTxns(IDataContext db, TransactionTrigger transactionTrigger, Public resolvedPublic, Journal journal, JournalTemplateTxn templateTxn)
        {
            foreach (var coverage in transactionTrigger.Policy.Coverages)
            {
                JournalTxn txn = new JournalTxn();
                txn.Amount = (ResolveAmount(db, transactionTrigger, resolvedPublic, templateTxn) * coverage.Premium.Value);
                txn.Description = templateTxn.Description + " - " + coverage.CoverageType.Name;
                txn.Policy = transactionTrigger.Policy;
                txn.Public = transactionTrigger.Public;
                //sp ??
                txn.ReportingEntityBranch = transactionTrigger.ReportingEntityBranch;
                txn.TransactionOrigin = transactionTrigger.TransactionOrigin;
                txn.TxnDate = transactionTrigger.TxnDate.Value;
                txn.JournalTemplateTxn = templateTxn;
                txn.PolicyCoverage = coverage;

                journal.JournalTxns.Add(txn);
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

            var query = db.LedgerTxns.Where(x => x.ReportingEntityID == transactionTrigger.ReportingEntityID && x.LedgerAccountID == templateTxn.AmountLedgerAccountID && x.TxnDate <= transactionTrigger.TxnDate);

            switch (templateTxn.BalanceOrigin)
            {
                case "P":
                    query = query.Where(x => x.PolicyID == transactionTrigger.PolicyID);
                    break;
                case "U":
                    query = query.Where(x => x.PublicID == resolvedPublic.ID);
                    break;
                    //todo: agents, sps, etc...
            }

            return query.Sum(x => x.Amount);
        }

        private decimal ResolveContextParameter(IDataContext db, TransactionTrigger trigger, JournalTemplateTxn templateTxn, Public resolvedPublic)
        {
            if (trigger.Policy != null)
            {
                return _contextParameterResolver.Resolve(db, trigger.TxnDate.Value, templateTxn.AmountContextParameterID.Value, trigger.Policy);
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
