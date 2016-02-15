using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeEntityRequirement : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public int PolicyTypeID { get; set; }
        public virtual PolicyType PolicyType { get; set; }

        public int ReportingEntityProfileID { get; set; }
        public virtual ReportingEntityProfile ReportingEntityProfile { get; set; }

        public int? DefaultReportingEntityID { get; set; }
        public virtual ReportingEntity DefaultReportingEntity { get; set; }
    }
}
