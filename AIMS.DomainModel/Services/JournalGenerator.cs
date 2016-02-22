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
        
        public JournalRunResult Run(IDataContext db, TransactionTrigger transactionTrigger)
        {
            JournalRunResult result = new JournalRunResult();                       

            Journal journal = new Journal();
            journal.Description = transactionTrigger.Description;
            journal.JournalType = transactionTrigger.JournalTemplate.JournalType;
            journal.ReportingEntity = transactionTrigger.ReportingEntity;
            journal.TransactionOrigin = transactionTrigger.TransactionOrigin;
                
            db.Journals.Add(journal);

            foreach (var templateTxn in transactionTrigger.JournalTemplate.JournalTemplateTxns)
            {
                if (templateTxn.JournalTxnClass.IsDefinedAmount)
                {
                    var amountInput = transactionTrigger.Inputs.First(x => x.JournalTemplateInputID == templateTxn.AmountInputID);
                    var amount = Convert.ToDecimal(amountInput.Value);

                    JournalTxn txn = new JournalTxn();
                    txn.Amount = amount;
                    txn.Description = templateTxn.Description;
                    txn.Policy = transactionTrigger.Policy;
                    txn.Public = transactionTrigger.Public;
                    txn.ReportingEntityBranch = transactionTrigger.ReportingEntityBranch;
                    txn.TransactionOrigin = transactionTrigger.TransactionOrigin;
                    txn.TxnDate = transactionTrigger.TxnDate.Value;                    

                    journal.JournalTxns.Add(txn);
                }
            }

            //switch (transactionTrigger.TransactionOrigin)
            //{
            //    case "P":                    
            //        break;

            //}

            transactionTrigger.Status = db.TransactionTriggerStatuses.First(x => x.Code == "S");

            result.Journals.Add(journal);
            return result;
        }

        
    }
        
}
