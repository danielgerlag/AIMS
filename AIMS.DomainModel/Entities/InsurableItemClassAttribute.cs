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
    public class InsurableItemClassAttribute : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Prompt { get; set; }

        public int DisplayOrder { get; set; }

        [StringLength(50)]
        public string AL3Group { get; set; }

        [StringLength(50)]
        public string AL3Element { get; set; }

        [Required]
        public AttributeType AttributeType { get; set; }

        public int? DecimalPlaces { get; set; }

        [Index]
        public int InsurableItemClassID { get; set; }
        public virtual InsurableItemClass InsurableItemClass { get; set; }
                
        public int? AttributeLookupListID { get; set; }
        public virtual AttributeLookupList AttributeLookupList { get; set; }


        public override string GetLookupText()
        {
            return Name;
        }
    }
}
