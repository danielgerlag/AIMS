using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeAgentRequirement : BaseEntity
    {        

        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }

        public int AgentTypeID { get; set; }
        public virtual AgentType AgentType { get; set; }

        [Required]
        public bool IsRequired { get; set; }
    }
}
