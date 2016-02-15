using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyType : BaseEntity
    {                

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }                

        public int RegionID { get; set; }
        public virtual Region Region { get; set; }

        public virtual ICollection<PolicyTypeEntityRequirement> EntityRequirements { get; set; } = new HashSet<PolicyTypeEntityRequirement>();

        public virtual ICollection<PolicyTypeAgentRequirement> AgentRequirements { get; set; } = new HashSet<PolicyTypeAgentRequirement>();

        public virtual ICollection<PolicyTypeServiceProvider> ServiceProviders { get; set; } = new HashSet<PolicyTypeServiceProvider>();

        public virtual ICollection<PolicySubType> PolicySubTypes { get; set; } = new HashSet<PolicySubType>();
    }
}
