using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class CoverageProfile : BaseEntity
    {
        public int ReportingEntityProfileID { get; set; }
        public virtual ReportingEntityProfile ReportingEntityProfile { get; set; }

        public int CoverageTypeID { get; set; }
        public virtual CoverageType CoverageType { get; set; }

        public int IncomeLedgerAccountID { get; set; }
        public virtual LedgerAccount IncomeLedgerAccount { get; set; }

        public int ExpenseLedgerAccountID { get; set; }
        public virtual LedgerAccount ExpenseLedgerAccount { get; set; }



    }
}
