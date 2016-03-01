using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalTxn : BaseEntity
    {

        public int JournalID { get; set; }
        public virtual Journal Journal { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

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

        public int? JournalTemplateTxnID { get; set; }
        public virtual JournalTemplateTxn JournalTemplateTxn { get; set; }

        public int? PolicyCoverageID { get; set; }
        public virtual PolicyCoverage PolicyCoverage { get; set; }

        [Required]
        public bool IsReconciled { get; set; }

        public virtual ICollection<LedgerTxn> LedgerTxns { get; set; } = new HashSet<LedgerTxn>();


    }
}
