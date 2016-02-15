using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ReportingEntity : BaseEntity
    {

        public int PublicID { get; set; }
        public virtual Public Public { get; set; }

        public int ReportingEntityProfileID { get; set; }
        public virtual ReportingEntityProfile ReportingEntityProfile { get; set; }

        public virtual ICollection<ReportingEntityBankAccount> ReportingEntityBankAccounts { get; set; } = new HashSet<ReportingEntityBankAccount>();

        public virtual ICollection<Journal> Journals { get; set; } = new HashSet<Journal>();
                
        public virtual ICollection<LedgerTxn> LedgerTxns { get; set; } = new HashSet<LedgerTxn>();
    }
}
