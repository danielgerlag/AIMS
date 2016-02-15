using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyReportingEntity : BaseEntity
    {

        public int PolicyID { get; set; }
        public virtual Policy Policy { get; set; }

        public int PolicyTypeEntityRequirementID { get; set; }
        public virtual PolicyTypeEntityRequirement PolicyTypeEntityRequirement { get; set; }

        public int? ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }
        

    }
}
