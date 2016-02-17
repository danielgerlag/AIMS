using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ServiceProviderType : BaseEntity
    {                

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public override string GetLookupText()
        {
            return Name;
        }
    }
}
