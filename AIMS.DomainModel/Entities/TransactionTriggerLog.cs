using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class TransactionTriggerLog : BaseEntity
    {

        public int TransactionTriggerID { get; set; }
        public virtual TransactionTrigger TransactionTrigger { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Thread { get; set; }

        public string MachineName { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsError { get; set; }

        public bool IsRequeue { get; set; }

        public virtual ICollection<TransactionTriggerException> Exceptions { get; set; } = new HashSet<TransactionTriggerException>();

    }
}
