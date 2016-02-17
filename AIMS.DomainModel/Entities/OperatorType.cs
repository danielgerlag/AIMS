using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class OperatorType : BaseEntity
    {
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
                

        public virtual ICollection<OperatorTypeAttributeGroup> Groups { get; set; } = new HashSet<OperatorTypeAttributeGroup>();

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
