using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeTransition : BaseEntity
    {

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string ButtonText { get; set; }

        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }

        public int? InitialStatusID { get; set; }
        public virtual PolicyTypeStatus InitialStatus { get; set; }

        public int TargetStatusID { get; set; }
        public virtual PolicyTypeStatus TargetStatus { get; set; }

        public string Script { get; set; }


        public virtual ICollection<PolicyTypeTransitionInput> Inputs { get; set; } = new HashSet<PolicyTypeTransitionInput>();

        public virtual ICollection<PolicyTypeTransitionJournalTemplate> JournalTemplates { get; set; } = new HashSet<PolicyTypeTransitionJournalTemplate>();


        public override string GetLookupText()
        {
            return Name;
        }
    }
}
