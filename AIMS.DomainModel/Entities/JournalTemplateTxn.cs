using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class JournalTemplateTxn : BaseEntity
    {

        public int JournalTemplateID { get; set; }
        public virtual JournalTemplate JournalTemplate { get; set; }

        public int JournalTxnClassID { get; set; }
        public virtual JournalTxnClass JournalTxnClass { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public int? PublicRequirementID { get; set; }
        public virtual PublicRequirement PublicRequirement { get; set; }

        public int? ServiceProviderTypeID { get; set; }
        public virtual ServiceProviderType ServiceProviderType { get; set; }

        public int? AgentTypeID { get; set; }
        public virtual AgentType AgentType { get; set; }

        public int? AmountInputID { get; set; }
        public virtual JournalTemplateInput AmountInput { get; set; }
        

        public int? AmountContextParameterID { get; set; }
        public virtual ContextParameter AmountContextParameter { get; set; }

        public int? AmountLedgerAccountID { get; set; }
        public virtual LedgerAccount AmountLedgerAccount { get; set; }

        public decimal? Amount { get; set; }
                

        [Required]
        [MaxLength(1)]  // G - Global, P - Policy, U - Public, A - Agent, B - Branch
        public string BalanceOrigin { get; set; }


        public virtual ICollection<JournalTemplateTxnPosting> Postings { get; set; } = new HashSet<JournalTemplateTxnPosting>();
    }
}
