using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class LedgerAccount : BaseEntity
    {
        public int ReportingEntityProfileID { get; set; }
        public virtual ReportingEntityProfile ReportingEntityProfile { get; set; }

        public int LedgerAccountTypeID { get; set; }
        public virtual LedgerAccountType LedgerAccountType { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
