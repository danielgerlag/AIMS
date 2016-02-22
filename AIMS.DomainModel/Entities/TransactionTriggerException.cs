using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class TransactionTriggerException : BaseEntity
    {

        public int TransactionTriggerLogID { get; set; }
        public virtual TransactionTriggerLog TransactionTriggerLog { get; set; }

        
        public string Message { get; set; }

        [Required]
        [MaxLength(1)]
        public string ExceptionType { get; set; }

    }
}
