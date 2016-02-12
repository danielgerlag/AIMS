using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    [Table("Party")]
    public class Party : BaseEntity
    {

        public Party()
        {
            ContactDetails = new HashSet<PartyContactDetail>();
        }


        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(1)]
        public string PartyType { get; set; }

        public virtual ICollection<PartyContactDetail> ContactDetails { get; set; }

        

        public override string GetLookupText()
        {
            switch (PartyType)
            {
                case "I":
                    return Name + ", " + FirstName;
                case "C":
                    return Name;
            }
            return Name;
        }

        
    }
}
