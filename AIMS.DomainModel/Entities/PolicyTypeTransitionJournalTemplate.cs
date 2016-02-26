using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeTransitionJournalTemplate : BaseEntity
    {
        [MaxLength(100)]
        public string Description { get; set; }

        public int PolicyTypeTransitionID { get; set; }
        public virtual PolicyTypeTransition PolicyTypeTransition { get; set; }
        
        public int EntityRequirementID { get; set; }
        public PolicyTypeEntityRequirement EntityRequirement { get; set; }

        public int JournalTemplateID { get; set; }
        public virtual JournalTemplate JournalTemplate { get; set; }
        
        public string Script { get; set; }

        public int? TxnDateInputID { get; set; }
        public virtual PolicyTypeTransitionInput TxnDateInput { get; set; }

        public int? TransactionTriggerStatusID { get; set; }
        public virtual TransactionTriggerStatus TransactionTriggerStatus { get; set; }

        public int? TransactionTriggerFrequencyID { get; set; }
        public virtual TransactionTriggerFrequency TransactionTriggerFrequency { get; set; }

        public virtual ICollection<PolicyTypeTransitionJournalTemplateInput> Inputs { get; set; } = new HashSet<PolicyTypeTransitionJournalTemplateInput>();
    }
}
