using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyCoverage : BaseEntity
    {

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int CoverageTypeID { get; set; }
        public virtual CoverageType CoverageType { get; set; }

        public int? InsurableItemID { get; set; }
        public virtual InsurableItem InsurableItem { get; set; }

        public decimal? Premium { get; set; }


    }
}
