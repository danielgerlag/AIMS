using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class TransactionTriggerInput : BaseEntity
    {

        public int TransactionTriggerID { get; set; }
        public virtual TransactionTrigger TransactionTrigger { get; set; }

        public int JournalTemplateInputID { get; set; }
        public virtual JournalTemplateInput JournalTemplateInput { get; set; }
        

        //[MaxLength(500)]
        //public string Description { get; set; }

    }
}
