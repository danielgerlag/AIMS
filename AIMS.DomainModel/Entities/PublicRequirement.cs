using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PublicRequirement : BaseEntity
    {                

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]        
        public bool IsServiceProvider{ get; set; }

        [Required]
        public bool IsAgent { get; set; }

        [Required]
        public bool IsPolicyHolder { get; set; }
    }
}
