using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ContactDetail : BaseEntity
    {

        [Index]
        public int PublicID { get; set; }

        public virtual Public Public { get; set; }

        [StringLength(500)]
        public string Data { get; set; }
        

    }
}
