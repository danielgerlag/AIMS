using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Public : BaseEntity
    {

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(1)]
        public string PartyType { get; set; }
        

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

        public virtual ICollection<BankAccount> BankAccounts { get; set; } = new HashSet<BankAccount>();

        public virtual ICollection<Journal> Journals { get; set; } = new HashSet<Journal>();

        public virtual ICollection<ContactDetail> ContactDetails { get; set; } = new HashSet<ContactDetail>();
    }
}
