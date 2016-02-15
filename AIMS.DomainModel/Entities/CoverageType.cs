using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class CoverageType : BaseEntity
    {                

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Code { get; set; }

        public int RegionID { get; set; }
        public virtual Region Region { get; set; }

    }
}
