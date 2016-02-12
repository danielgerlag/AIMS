using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AIMS.DomainModel.Abstractions.Entities;

namespace AIMS.DomainModel.Entities
{
    public class AttributeLookupList : BaseEntity
    {

        public AttributeLookupList()
        {
            Items = new HashSet<AttributeLookupItem>();
        }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(100)]
        public string CSIOCode { get; set; }

        public virtual ICollection<AttributeLookupItem> Items { get; set; }

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
