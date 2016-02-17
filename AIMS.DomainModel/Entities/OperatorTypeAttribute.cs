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
    public class OperatorTypeAttribute : BaseEntity
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

        public int AttributeDataTypeID { get; set; }
        public virtual AttributeDataType AttributeDataType { get; set; }

        public int? DecimalPlaces { get; set; }

        public bool Required { get; set; }

        public bool IndexField { get; set; }

        [Index]
        public int OperatorTypeAttributeGroupID { get; set; }
        public virtual OperatorTypeAttributeGroup OperatorTypeAttributeGroup { get; set; }
                
        public int? AttributeLookupListID { get; set; }
        public virtual AttributeLookupList AttributeLookupList { get; set; }


        public override string GetLookupText()
        {
            return Name;
        }
    }
}
