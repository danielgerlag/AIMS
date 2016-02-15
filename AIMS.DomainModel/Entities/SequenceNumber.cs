using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class SequenceNumber : BaseEntity
    {                

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public int NextValue { get; set; }

        [Required]
        [MaxLength(20)]
        public string FormatMask { get; set; }
    }
}
