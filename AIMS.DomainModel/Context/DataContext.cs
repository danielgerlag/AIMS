using AIMS.DomainModel.Context;
using AIMS.Services.Indexer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Abstractions;

namespace AIMS.DomainModel.Context
{
    public class DataContext : BaseContext, IDataContext
    {

        public virtual DbSet<AttributeDataType> AttributeDataTypes { get; set; }
        public virtual DbSet<AttributeLookupItem> AttributeLookupItems { get; set; }
        public virtual DbSet<AttributeLookupList> AttributeLookupLists { get; set; }
        public virtual DbSet<AttributeLookupSubList> AttributeLookupSubLists { get; set; }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentType> AgentTypes { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BusinessLine> BusinessLines { get; set; }
        public virtual DbSet<ContactDetail> ContactDetails { get; set; }
        public virtual DbSet<ContextParameter> ContextParameters { get; set; }
        public virtual DbSet<CoverageProfile> CoverageProfiles { get; set; }
        public virtual DbSet<CoverageType> CoverageTypes { get; set; }
        public virtual DbSet<InsurableItem> InsurableItems { get; set; }
        public virtual DbSet<InsurableItemAttribute> InsurableItemAttributes { get; set; }
        public virtual DbSet<InsurableItemClass> InsurableItemClasses { get; set; }
        public virtual DbSet<InsurableItemClassAttribute> InsurableItemClassAttributes { get; set; }
        public virtual DbSet<InsurableItemClassOperatorType> InsurableItemClassOperatorTypes { get; set; }

        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<JournalTemplate> JournalTemplates { get; set; }
        public virtual DbSet<JournalTemplateInput> JournalTemplateInputs { get; set; }
        public virtual DbSet<JournalTemplateTxn> JournalTemplateTxns { get; set; }
        public virtual DbSet<JournalTemplateTxnPosting> JournalTemplateTxnPostings { get; set; }
        public virtual DbSet<JournalTxn> JournalTxns { get; set; }
        public virtual DbSet<JournalTxnClass> JournalTxnClasses { get; set; }
        public virtual DbSet<JournalType> JournalTypes { get; set; }

        public virtual DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public virtual DbSet<LedgerAccountType> LedgerAccountTypes { get; set; }
        public virtual DbSet<LedgerTxn> LedgerTxns { get; set; }

        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<OperatorType> OperatorTypes { get; set; }
        public virtual DbSet<OperatorTypeAttribute> OperatorTypeAttributes { get; set; }
        public virtual DbSet<OperatorAttribute> OperatorAttributes { get; set; }
        public virtual DbSet<PolicyAgent> PolicyAgents { get; set; }
        public virtual DbSet<PolicyCoverage> PolicyCoverages { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PolicyHolder> PolicyHolders { get; set; }
        public virtual DbSet<PolicyRiskLocation> PolicyRiskLocations { get; set; }
        public virtual DbSet<PolicyReportingEntity> PolicyReportingEntities { get; set; }
        public virtual DbSet<PolicyServiceProvider> PolicyServiceProviders { get; set; }

        public virtual DbSet<PolicySubType> PolicySubTypes { get; set; }
        public virtual DbSet<PolicyType> PolicyTypes { get; set; }
        public virtual DbSet<PolicyTypeAgentRequirement> PolicyTypeAgentRequirements { get; set; }
        public virtual DbSet<PolicyTypeEntityRequirement> PolicyTypeEntityRequirements { get; set; }
        public virtual DbSet<PolicyTypeServiceProvider> PolicyTypeServiceProviders { get; set; }

        public virtual DbSet<Public> Publics { get; set; }
        public virtual DbSet<PublicRequirement> PublicRequirements { get; set; }

        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ReportingEntity> ReportingEntities { get; set; }
        public virtual DbSet<ReportingEntityBankAccount> ReportingEntityBankAccounts { get; set; }
        public virtual DbSet<ReportingEntityBranch> ReportingEntityBranches { get; set; }
        public virtual DbSet<ReportingEntityProfile> ReportingEntityProfiles { get; set; }
        public virtual DbSet<RiskLocation> RiskLocations { get; set; }

        public virtual DbSet<SequenceNumber> SequenceNumbers { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }
        public virtual DbSet<ServiceProviderType> ServiceProviderTypes { get; set; }

        public virtual DbSet<TransactionTrigger> TransactionTriggers { get; set; }
        public virtual DbSet<TransactionTriggerFrequency> TransactionTriggerFrequencies { get; set; }
        public virtual DbSet<TransactionTriggerInput> TransactionTriggerInputs { get; set; }
        public virtual DbSet<TransactionTriggerStatus> TransactionTriggerStatuses { get; set; }


        public virtual DbSet<PolicyStatus> PolicyStatuses { get; set; }


        protected override Type GetInterfaceType()
        {
            return typeof(IDataContext); //TODO: interface
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<DataContext>(new DatabaseInit());
        }

        public DataContext(IIndexQueue indexQueue, IIndexRegister indexStoreInit)
            : base(indexQueue, indexStoreInit)
        {
            Database.SetInitializer<DataContext>(new DatabaseInit());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Entities.Party>().Ignore<bool>(x => x.Deleted);
            //modelBuilder.Entity<Entities.PartyContactDetail>().Ignore<bool>(x => x.Deleted);
        }

    }
}
