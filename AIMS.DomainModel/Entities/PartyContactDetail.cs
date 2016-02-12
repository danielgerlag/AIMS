using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    [Table("PartyContactDetail")]
    public class PartyContactDetail : BaseEntity
    {

        [Index]
        public int PartyID { get; set; }

        public virtual Party Party { get; set; }

        [StringLength(500)]
        public string Data { get; set; }


        //public PartyType Type { get; set; }

    }
}
