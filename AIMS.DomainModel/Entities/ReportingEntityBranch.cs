using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ReportingEntityBranch : BaseEntity
    {
        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Agent> Agents { get; set; } = new HashSet<Agent>();

        public virtual ICollection<Journal> Journals { get; set; } = new HashSet<Journal>();

        public virtual ICollection<LedgerTxn> LedgerTxns { get; set; } = new HashSet<LedgerTxn>();

        public virtual ICollection<TransactionTrigger> TransactionTriggers { get; set; } = new HashSet<TransactionTrigger>();
    }
}
