using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class RatingProfile : BaseEntity
    {                

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }


        [Required]
        [MaxLength(50)]
        public string Engine { get; set; }

        
        [MaxLength(50)]
        public string ScriptLanguage { get; set; }

        public string Script { get; set; }
    }
}
