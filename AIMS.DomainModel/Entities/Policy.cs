using AIMS.DomainModel.Abstractions.Entities;
using AIMS.DomainModel.Abstractions.Intercepts;
using AIMS.DomainModel.Intercepts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Entities
{
    [Intercept(Intercept = typeof(GeneratePolicyNumber), Stage = Stage.OnAddBeforeCommit, Order = 1)]
    public class Policy : BaseEntity
    {

        [MaxLength(100)]
        public string PolicyNumber { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int PolicySubTypeID { get; set; }
        public virtual PolicySubType PolicySubType { get; set; }

        [Index]
        public int? BillingPublicID { get; set; }
        public virtual Public BillingPublic { get; set; }


        public virtual ICollection<PolicyHolder> PolicyHolders { get; set; } = new HashSet<PolicyHolder>();

        public virtual ICollection<PolicyCoverage> Coverages { get; set; } = new HashSet<PolicyCoverage>();

        public virtual ICollection<PolicyRiskLocation> RiskLocations { get; set; } = new HashSet<PolicyRiskLocation>();

        public virtual ICollection<InsurableItem> InsurableItems { get; set; } = new HashSet<InsurableItem>();

        public virtual ICollection<Journal> Journals { get; set; } = new HashSet<Journal>();

        public virtual ICollection<LedgerTxn> LedgerTxns { get; set; } = new HashSet<LedgerTxn>();


        public virtual ICollection<PolicyReportingEntity> ReportingEntities { get; set; } = new HashSet<PolicyReportingEntity>();

        public virtual ICollection<PolicyServiceProvider> ServiceProviders { get; set; } = new HashSet<PolicyServiceProvider>();

        public virtual ICollection<PolicyAgent> Agents { get; set; } = new HashSet<PolicyAgent>();

        public virtual ICollection<Operator> Operators { get; set; } = new HashSet<Operator>();

        public virtual ICollection<TransactionTrigger> TransactionTriggers { get; set; } = new HashSet<TransactionTrigger>();

        public virtual ICollection<PolicyContextParameterValue> ContextParameterValues { get; set; } = new HashSet<PolicyContextParameterValue>();
    }
}
