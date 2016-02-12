using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Broker : BaseEntity
    {
        public Broker()
        {
            BrokerDataAreas = new HashSet<BrokerDataArea>();
        }

        public int PartyID { get; set; }
        public virtual Party Party { get; set; }

        public int BrokerageBranchID { get; set; }
        public virtual BrokerageBranch BrokerageBranch { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        public virtual ICollection<BrokerDataArea> BrokerDataAreas { get; set; }

    }

}
