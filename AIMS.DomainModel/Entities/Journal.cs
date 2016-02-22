using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Journal : BaseEntity
    {

        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

        public int JournalTypeID { get; set; }
        public virtual JournalType JournalType { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Reference { get; set; }

        [Required]
        [MaxLength(1)]  // G - Global, P - Policy, U - Public, A - Agent, B - Branch
        public string TransactionOrigin { get; set; }

        public virtual ICollection<JournalTxn> JournalTxns { get; set; } = new HashSet<JournalTxn>();

    }
}
