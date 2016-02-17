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
    public class InsurableItemClass : BaseEntity
    {        

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string DisplayName { get; set; }

        [Index]
        public int? ParentInsurableItemClassID { get; set; }
        public virtual InsurableItemClass ParentInsurableItemClass { get; set; }
                
        public virtual ICollection<InsurableItemClassOperatorType> OperatorTypes { get; set; } = new HashSet<InsurableItemClassOperatorType>();

        public virtual ICollection<InsurableItemClassAttributeGroup> Groups { get; set; } = new HashSet<InsurableItemClassAttributeGroup>();

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
