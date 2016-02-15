using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ServiceProvider : BaseEntity
    {

        public int PublicID { get; set; }
        public virtual Public Public { get; set; }

        public int ServiceProviderTypeID { get; set; }
        public virtual ServiceProviderType ServiceProviderType { get; set; }                

        public virtual ICollection<Journal> Journals { get; set; } = new HashSet<Journal>();
    }
}
