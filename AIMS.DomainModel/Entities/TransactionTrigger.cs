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

        [Required]
        [MaxLength(1)]  // G - Global, P - Policy, U - Public, A - Agent, B - Branch
        public string TransactionOrigin { get; set; }

        public int TransactionTriggerFrequencyID { get; set; }
        public virtual TransactionTriggerFrequency TransactionTriggerFrequency { get; set; }

        public int TransactionTriggerStatusID { get; set; }
        public virtual TransactionTriggerStatus TransactionTriggerStatus { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public DateTime? TxnDate { get; set; }

        public DateTime? NextExecutionDate { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public virtual ICollection<TransactionTriggerInput> Inputs { get; set; } = new HashSet<TransactionTriggerInput>();


        public int? PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int? PublicID { get; set; }
        public virtual Public Public { get; set; }

        public int? ServiceProviderID { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }

        public int? AgentID { get; set; }
        public virtual Agent Agent { get; set; }

        public int? ReportingEntityBranchID { get; set; }
        public virtual ReportingEntityBranch ReportingEntityBranch { get; set; }
    }
}
