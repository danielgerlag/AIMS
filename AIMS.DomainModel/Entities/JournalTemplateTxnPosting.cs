using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalTemplateTxnPosting : BaseEntity
    {

        public int JournalTemplateTxnID { get; set; }
        public virtual JournalTemplateTxn JournalTemplateTxn { get; set; }

        public int LedgerAccountID { get; set; }
        public virtual LedgerAccount LedgerAccount { get; set; }

        [Required]
        [MaxLength(1)]
        public string PostType { get; set; }

        [Required]        
        public bool AddBaseAmount { get; set; }

        [Required]
        public bool InheritPolicy { get; set; }

        [Required]
        public bool InheritPublic { get; set; }


    }
}
