using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class AgentType : BaseEntity
    {

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        
        public override string GetLookupText()
        {
            return Description;
        }
    }
}
