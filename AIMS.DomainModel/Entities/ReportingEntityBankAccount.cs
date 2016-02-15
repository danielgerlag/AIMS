using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ReportingEntityBankAccount : BaseEntity
    {
        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

        public int BankAccountID { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        

    }
}
