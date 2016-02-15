using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeServiceProvider : BaseEntity
    {

        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }

        public int ServiceProviderTypeID { get; set; }
        public virtual ServiceProviderType ServiceProviderType { get; set; }

        public int? DefaultServiceProviderID { get; set; }
        public virtual ServiceProvider DefaultServiceProvider { get; set; }
    }
}
