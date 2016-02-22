using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class JournalPoster : IJournalPoster
    {
        
        public JournalRunResult Run(IDataContext db, Journal journal)
        {
            JournalRunResult result = new JournalRunResult();

            foreach (var journalTxn in journal.JournalTxns)
            {
                foreach (var posting in journalTxn.JournalTemplateTxn.Postings)
                {
                    LedgerTxn ledgerTxn = new LedgerTxn();
                    
                    ledgerTxn.LedgerAccount = posting.LedgerAccount;

                    if ((posting.CoverageExpenseAccount) && (journalTxn.PolicyCoverage != null))
                        ledgerTxn.LedgerAccount = ResolveCoverageExpenseAccount(journal.ReportingEntity, journalTxn.PolicyCoverage);

                    if ((posting.CoverageIncomeAccount) && (journalTxn.PolicyCoverage != null))
                        ledgerTxn.LedgerAccount = ResolveCoverageIncomeAccount(journal.ReportingEntity, journalTxn.PolicyCoverage);

                    if (ledgerTxn.LedgerAccount == null)
                        throw new Exception("Ledger account not specified");
                                       
                    ledgerTxn.TxnDate = journalTxn.TxnDate;
                    ledgerTxn.TransactionOrigin = journal.TransactionOrigin;
                    ledgerTxn.ReportingEntityID = journal.ReportingEntityID;
                    ledgerTxn.ReportingEntityBranch = journalTxn.ReportingEntityBranch;
                    ledgerTxn.Agent = journalTxn.Agent;

                    if (posting.InheritPolicy)
                        ledgerTxn.Policy = journalTxn.Policy;

                    if (posting.InheritPublic)
                        ledgerTxn.Public = journalTxn.Public;


                    int multiplier = 0;
                    if (ledgerTxn.LedgerAccount.LedgerAccountType.CreditPositive)
                    {
                        if (posting.PostType == "C")
                            multiplier = 1;
                        else
                            multiplier = -1;
                    }
                    else                    
                    {
                        if (posting.PostType == "D")
                            multiplier = 1;
                        else
                            multiplier = -1;
                    }
                    
                    decimal amount = 0;

                    if (posting.AddBaseAmount)
                        amount += journalTxn.Amount;

                    ledgerTxn.Amount = (amount * multiplier);

                    journalTxn.LedgerTxns.Add(ledgerTxn);
                }
            }

            return result;
        }


        private LedgerAccount ResolveCoverageExpenseAccount(ReportingEntity entity, PolicyCoverage coverage)
        {
            var coverageProfile = entity.ReportingEntityProfile.CoverageProfiles.FirstOrDefault(x => x.CoverageTypeID == coverage.CoverageTypeID);
            if (coverageProfile != null)
                return coverageProfile.ExpenseLedgerAccount;

            throw new Exception("Coverage profile not found - " + coverageProfile.CoverageType.Name);                
        }

        private LedgerAccount ResolveCoverageIncomeAccount(ReportingEntity entity, PolicyCoverage coverage)
        {
            var coverageProfile = entity.ReportingEntityProfile.CoverageProfiles.FirstOrDefault(x => x.CoverageTypeID == coverage.CoverageTypeID);
            if (coverageProfile != null)
                return coverageProfile.IncomeLedgerAccount;

            throw new Exception("Coverage profile not found - " + coverageProfile.CoverageType.Name);
        }

    }
        
}
