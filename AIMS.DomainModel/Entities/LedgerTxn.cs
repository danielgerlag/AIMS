﻿using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class LedgerTxn : BaseEntity
    {
        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

        public int JournalTxnID { get; set; }
        public virtual JournalTxn JournalTxn { get; set; }

        public int LedgerAccountID { get; set; }
        public virtual LedgerAccount LedgerAccount { get; set; }


        //[Required]
        //[MaxLength(1)]  // G - Global, P - Policy, U - Public, A - Agent, B - Branch
        //public string TransactionOrigin { get; set; }

        public DateTime TxnDate { get; set; }

        public decimal Amount { get; set; }

        public int? PublicID { get; set; }
        public virtual Public Public { get; set; }

        public int? PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int? AgentID { get; set; }
        public virtual Agent Agent { get; set; }

        public int? ReportingEntityBranchID { get; set; }
        public virtual ReportingEntityBranch ReportingEntityBranch { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal AbsAmount
        {
            get
            {
                return Math.Abs(Amount); 
            }
            private set { }
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Description
        {
            get
            {
                if (LedgerAccount != null)
                {
                    if (LedgerAccount.LedgerAccountType != null)
                    {
                        if (LedgerAccount.LedgerAccountType.CreditPositive)
                        {
                            if (Amount >= 0)
                                return "Credit";
                            else
                                return "Debit";
                        }
                        else
                        {
                            if (Amount >= 0)
                                return "Debit";
                            else
                                return "Credit";
                        }
                    }
                }
                return string.Empty;
            }
            private set { }
        }

    }
}
