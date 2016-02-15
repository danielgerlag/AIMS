using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class TransactionTrigger : BaseEntity
    {
        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

        public int JournalTemplateID { get; set; }
        public virtual JournalTemplate JournalTemplate { get; set; }

        public int TransactionTriggerFrequencyID { get; set; }
        public virtual TransactionTriggerFrequency TransactionTriggerFrequency { get; set; }

        public int TransactionTriggerStatusID { get; set; }
        public virtual TransactionTriggerStatus TransactionTriggerStatus { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public DateTime? TxnDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public virtual ICollection<TransactionTriggerInput> Inputs { get; set; } = new HashSet<TransactionTriggerInput>();
    }
}
