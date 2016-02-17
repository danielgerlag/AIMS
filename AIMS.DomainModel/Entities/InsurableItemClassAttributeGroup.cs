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
    public class InsurableItemClassAttributeGroup : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Prompt { get; set; }

        public int DisplayOrder { get; set; }
        

        [Index]
        public int InsurableItemClassID { get; set; }
        public virtual InsurableItemClass InsurableItemClass { get; set; }

        public virtual ICollection<InsurableItemClassAttribute> Attributes { get; set; } = new HashSet<InsurableItemClassAttribute>();

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
