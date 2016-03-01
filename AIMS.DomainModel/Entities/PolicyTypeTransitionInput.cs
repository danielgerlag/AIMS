using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeTransitionInput : BaseEntity
    {
        public int PolicyTypeTransitionID { get; set; }
        public virtual PolicyTypeTransition PolicyTypeTransition { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Prompt { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int AttributeDataTypeID { get; set; }
        public virtual AttributeDataType AttributeDataType { get; set; }

        public bool Required { get; set; }

        public int? AttributeLookupListID { get; set; }
        public virtual AttributeLookupList AttributeLookupList { get; set; }



        public override string GetLookupText()
        {
            return Name;
        }
    }
}
