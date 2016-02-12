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
        public InsurableItemClass()
        {
            Attributes = new HashSet<InsurableItemClassAttribute>();
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Index]
        public int? ParentInsurableItemClassID { get; set; }
        public virtual InsurableItemClass ParentInsurableItemClass { get; set; }
                
        public int? OperatorTypeID { get; set; }
        public virtual OperatorType OperatorType { get; set; }

        public virtual ICollection<InsurableItemClassAttribute> Attributes { get; set; }

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
