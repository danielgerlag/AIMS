using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ContextParameter : BaseEntity
    {                

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        public int DecimalPlaces { get; set; }

        [Required]
        public bool IsPercentage { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string UserPrompt { get; set; }
    }
}
