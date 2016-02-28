using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class RegionTax : BaseEntity
    {
        public int RegionID { get; set; }
        public virtual Region Region { get; set; }

        public int ContextParameterID { get; set; }
        public virtual ContextParameter ContextParameter { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

    }
}
