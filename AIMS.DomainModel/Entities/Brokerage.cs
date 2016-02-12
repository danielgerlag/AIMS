using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class Brokerage : BaseEntity
    {
        public Brokerage()
        {
            Branches = new HashSet<BrokerageBranch>();
        }

        public int PartyID { get; set; }
        public virtual Party Party { get; set; }

        public Guid OwnerKey { get; set; }

        public virtual ICollection<BrokerageBranch> Branches { get; set; }
    }
}
