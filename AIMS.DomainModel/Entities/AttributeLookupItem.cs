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
    public class AttributeLookupItem : BaseEntity
    {
     
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(100)]
        public string CSIOCode { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [Index]
        public int AttributeLookupListID { get; set; }
        public virtual AttributeLookupList AttributeLookupList { get; set; }

        [Index]
        public int? AttributeLookupSubListID { get; set; }
        public virtual AttributeLookupSubList AttributeLookupSubList { get; set; }


        public override string GetLookupText()
        {
            return Description;
        }
    }
}
