using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    //[Table("BrokerDataArea")]
    public class BrokerDataArea : BaseEntity
    {

        public int BrokerID { get; set; }

        public virtual Broker Broker { get; set; }

        [Required]
        public bool Read { get; set; }

        [Required]
        public bool Write { get; set; }

    }
}
