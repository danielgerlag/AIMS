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
        public OperatorType()
        {
            Attributes = new HashSet<OperatorTypeAttribute>();
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
                

        public virtual ICollection<OperatorTypeAttribute> Attributes { get; set; }

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
