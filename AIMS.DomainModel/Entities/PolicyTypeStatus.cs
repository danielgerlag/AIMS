using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeStatus : BaseEntity
    {

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool AllowRating { get; set; }

        public bool AllowChanges { get; set; }

        public bool AllowTransactions { get; set; }

        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }


        public override string GetLookupText()
        {
            return Name;
        }
    }
}
