using AIMS.DomainModel.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    public class ReportingEntityProfile : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public virtual ICollection<LedgerAccount> LedgerAccounts { get; set; } = new HashSet<LedgerAccount>();

        public virtual ICollection<CoverageProfile> CoverageProfiles { get; set; } = new HashSet<CoverageProfile>();

        public virtual ICollection<JournalTemplate> JournalTemplates { get; set; } = new HashSet<JournalTemplate>();

        public virtual ICollection<ReportingEntity> ReportingEntities { get; set; } = new HashSet<ReportingEntity>();

    }
}
