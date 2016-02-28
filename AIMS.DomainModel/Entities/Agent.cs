using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Agent : BaseEntity
    {

        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

        public int? ReportingEntityBranchID { get; set; }
        public virtual ReportingEntityBranch ReportingEntityBranch { get; set; }

        public int PublicID { get; set; }
        public virtual Public Public { get; set; }

        public int AgentTypeID { get; set; }
        public virtual AgentType AgentType { get; set; }

        public virtual ICollection<Journal> Journals { get; set; } = new HashSet<Journal>();

        public virtual ICollection<LedgerTxn> LedgerTxns { get; set; } = new HashSet<LedgerTxn>();

        public virtual ICollection<TransactionTrigger> TransactionTriggers { get; set; } = new HashSet<TransactionTrigger>();
    }
}
