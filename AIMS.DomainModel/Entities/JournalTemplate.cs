using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalTemplate : BaseEntity
    {

        public int ReportingEntityProfileID { get; set; }
        public virtual ReportingEntityProfile ReportingEntityProfile { get; set; }

        public int JournalTypeID { get; set; }
        public virtual JournalType JournalType { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1)]  // G - Global, P - Policy, U - Public, A - Agent, B - Branch
        public string TransactionOrigin { get; set; }

        public int Priority { get; set; }

        public int? SequenceNumberID { get; set; }
        public virtual SequenceNumber SequenceNumber { get; set; }

        public int? ReferenceInputID { get; set; }
        public virtual JournalTemplateInput ReferenceInput { get; set; }


        public virtual ICollection<JournalTemplateTxn> JournalTemplateTxns { get; set; } = new HashSet<JournalTemplateTxn>();

        public virtual ICollection<JournalTemplateInput> Inputs { get; set; } = new HashSet<JournalTemplateInput>();

    }
}
