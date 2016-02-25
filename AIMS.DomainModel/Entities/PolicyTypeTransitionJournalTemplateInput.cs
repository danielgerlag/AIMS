using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class PolicyTypeTransitionJournalTemplateInput : BaseEntity
    {
        public int PolicyTypeTransitionJournalTemplateID { get; set; }
        public virtual PolicyTypeTransitionJournalTemplate PolicyTypeTransitionJournalTemplate { get; set; }


        public int JournalTemplateInputID { get; set; }
        public virtual JournalTemplateInput JournalTemplateInput { get; set; }

        public int? PolicyTypeTransitionInputID { get; set; }
        public virtual PolicyTypeTransitionInput PolicyTypeTransitionInput { get; set; }


        public string Expression { get; set; }

    }
}
