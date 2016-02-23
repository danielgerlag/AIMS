using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyHolder : BaseEntity
    {
        public PolicyHolder()
        {            
        }

        public int PublicID { get; set; }
        public virtual Public Public { get; set; }

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public decimal? BillingPercent { get; set; }

    }

}
