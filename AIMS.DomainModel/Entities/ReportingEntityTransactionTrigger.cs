using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ReportingEntityTransactionTrigger : BaseEntity
    {
        public int TransactionTriggerID { get; set; }
        public virtual TransactionTrigger TransactionTrigger { get; set; }

        public int ReportingEntityID { get; set; }
        public virtual ReportingEntity ReportingEntity { get; set; }

    }
}
