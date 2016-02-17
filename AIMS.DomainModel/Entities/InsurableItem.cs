using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class InsurableItem : BaseEntity
    {

        public InsurableItem()
        {
        
        }                
        
        [Index]
        public int InsurableItemClassID { get; set; }
        public virtual InsurableItemClass InsurableItemClass { get; set; }

        [Index]
        public int PolicyRiskLocationID { get; set; }
        public virtual PolicyRiskLocation PolicyRiskLocation { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string SerialNo { get; set; }

        public virtual ICollection<InsurableItemAttribute> Attributes { get; set; } = new HashSet<InsurableItemAttribute>();

        public virtual ICollection<InsurableItemOperator> Operators { get; set; } = new HashSet<InsurableItemOperator>();

        public virtual ICollection<PolicyCoverage> PolicyCoverages { get; set; } = new HashSet<PolicyCoverage>();

        public override string GetLookupText()
        {
            return Description;
        }


    }
}
