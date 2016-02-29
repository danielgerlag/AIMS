using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ContactDetailType : BaseEntity
    {                
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string RegularExpression { get; set; }

        public bool IsPhone { get; set; }

        public bool IsMobile { get; set; }

        public bool IsFixed { get; set; }

        public bool IsBusiness { get; set; }

        public bool IsAddress { get; set; }

        public bool IsFax { get; set; }

        public bool IsEmail { get; set; }

        public bool IsTwitter { get; set; }

        public bool IsFacebook { get; set; }

    }
}
