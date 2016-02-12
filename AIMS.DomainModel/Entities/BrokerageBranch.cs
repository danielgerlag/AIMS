using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class BrokerageBranch : BaseEntity
    {
        public BrokerageBranch()
        {
            Brokers = new HashSet<Broker>();
        }

        public int BrokerageID { get; set; }
        public virtual Brokerage Brokerage { get; set; }

        public virtual ICollection<Broker> Brokers { get; set; }
    }
}
