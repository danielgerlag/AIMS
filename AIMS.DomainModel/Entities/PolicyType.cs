using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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


        public int? InitialStatusID { get; set; }
        public virtual PolicyTypeStatus InitialStatus { get; set; }

        public virtual ICollection<PolicyTypeEntityRequirement> EntityRequirements { get; set; } = new HashSet<PolicyTypeEntityRequirement>();

        public virtual ICollection<PolicyTypeAgentRequirement> AgentRequirements { get; set; } = new HashSet<PolicyTypeAgentRequirement>();

        public virtual ICollection<PolicyTypeServiceProvider> ServiceProviders { get; set; } = new HashSet<PolicyTypeServiceProvider>();

        public virtual ICollection<PolicySubType> PolicySubTypes { get; set; } = new HashSet<PolicySubType>();

        public virtual ICollection<PolicyTypeItemClass> ItemClasses { get; set; } = new HashSet<PolicyTypeItemClass>();

        public virtual ICollection<PolicyTypeContextParameterValue> ContextParameterValues { get; set; } = new HashSet<PolicyTypeContextParameterValue>();

        [InverseProperty("PolicyType")]
        public virtual ICollection<PolicyTypeStatus> Statuses { get; set; } = new HashSet<PolicyTypeStatus>();

        public virtual ICollection<PolicyTypeTransition> Transitions { get; set; } = new HashSet<PolicyTypeTransition>();
    }
}
