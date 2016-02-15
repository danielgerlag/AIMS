using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyServiceProvider : BaseEntity
    {

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int ServiceProviderTypeID { get; set; }
        public virtual ServiceProviderType ServiceProviderType { get; set; }

        public int ServiceProviderID { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }

        public decimal Percentage { get; set; }


    }
}
