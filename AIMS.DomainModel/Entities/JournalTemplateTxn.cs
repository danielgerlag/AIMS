using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalTemplateTxn : BaseEntity
    {

        public int JournalTemplateID { get; set; }
        public virtual JournalTemplate JournalTemplate { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public int? PublicRequirementID { get; set; }
        public virtual PublicRequirement PublicRequirement { get; set; }

        public int? ServiceProviderTypeID { get; set; }
        public virtual ServiceProviderType ServiceProviderType { get; set; }

        public int? AgentTypeID { get; set; }
        public virtual AgentType AgentType { get; set; }



    }
}
