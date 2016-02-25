using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Models
{
    public class LedgerAccountBalance
    {
        public LedgerAccountBalance()
        {
        }

        public LedgerAccountBalance(Policy policy)
        {
            if (policy != null)
            {
                PolicyID = policy.ID;
                PolicyNumber = policy.PolicyNumber;
            }
        }

        public LedgerAccountBalance(Public publ)
        {
            if (publ != null)
            {
                PublicID = publ.ID;
                PublicName = publ.GetLookupText();
            }
        }

        public LedgerAccountBalance(Public publ, Policy policy)
        {
            if (publ != null)
            {
                PublicID = publ.ID;
                PublicName = publ.GetLookupText();
            }

            if (policy != null)
            {
                PolicyID = policy.ID;
                PolicyNumber = policy.PolicyNumber;
            }
        }


        public DateTime EffectiveDate { get; set; }

        public int LedgerAccountID { get; set; }
        public string LedgerAccountName { get; set; }
        public int ReportingEntityID { get; set; }
        public string ReportingEntityName { get; set; }

        public int PublicID { get; set; }
        public string PublicName { get; set; }

        public int PolicyID { get; set; }
        public string PolicyNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
