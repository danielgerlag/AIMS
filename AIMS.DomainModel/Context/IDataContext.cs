using System.Data.Entity;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Interface;

namespace AIMS.DomainModel.Context
{
    public interface IDataContext : IDbContext
    {

        DbSet<AttributeDataType> AttributeDataTypes { get; set; }
        DbSet<AttributeLookupItem> AttributeLookupItems { get; set; }
        DbSet<AttributeLookupList> AttributeLookupLists { get; set; }
        DbSet<AttributeLookupSubList> AttributeLookupSubLists { get; set; }

        DbSet<Agent> Agents { get; set; }
        DbSet<AgentType> AgentTypes { get; set; }
        DbSet<BankAccount> BankAccounts { get; set; }
        DbSet<BusinessLine> BusinessLines { get; set; }
        DbSet<ContactDetail> ContactDetails { get; set; }
        DbSet<ContextParameter> ContextParameters { get; set; }
        DbSet<CoverageProfile> CoverageProfiles { get; set; }
        DbSet<CoverageType> CoverageTypes { get; set; }
        DbSet<InsurableItem> InsurableItems { get; set; }
        DbSet<InsurableItemAttribute> InsurableItemAttributes { get; set; }
        DbSet<InsurableItemClass> InsurableItemClasses { get; set; }
        DbSet<InsurableItemClassAttribute> InsurableItemClassAttributes { get; set; }
        DbSet<InsurableItemClassAttributeGroup> InsurableItemClassAttributeGroups { get; set; }
        DbSet<InsurableItemClassOperatorType> InsurableItemClassOperatorTypes { get; set; }
        DbSet<InsurableItemOperator> InsurableItemOperators { get; set; }

        DbSet<Journal> Journals { get; set; }
        DbSet<JournalTemplate> JournalTemplates { get; set; }
        DbSet<JournalTemplateInput> JournalTemplateInputs { get; set; }
        DbSet<JournalTemplateTxn> JournalTemplateTxns { get; set; }
        DbSet<JournalTemplateTxnPosting> JournalTemplateTxnPostings { get; set; }
        DbSet<JournalTxn> JournalTxns { get; set; }
        DbSet<JournalTxnClass> JournalTxnClasses { get; set; }
        DbSet<JournalType> JournalTypes { get; set; }

        DbSet<LedgerAccount> LedgerAccounts { get; set; }
        DbSet<LedgerAccountType> LedgerAccountTypes { get; set; }
        DbSet<LedgerTxn> LedgerTxns { get; set; }
        DbSet<Ledger> Ledgers { get; set; }

        DbSet<Operator> Operators { get; set; }
        DbSet<OperatorType> OperatorTypes { get; set; }
        DbSet<OperatorTypeAttribute> OperatorTypeAttributes { get; set; }
        DbSet<OperatorAttribute> OperatorAttributes { get; set; }
        DbSet<OperatorTypeAttributeGroup> OperatorTypeAttributeGroups { get; set; }
        DbSet<PolicyAgent> PolicyAgents { get; set; }
        DbSet<PolicyCoverage> PolicyCoverages { get; set; }                
        DbSet<Policy> Policies { get; set; }
        DbSet<PolicyHolder> PolicyHolders { get; set; }
        DbSet<PolicyRiskLocation> PolicyRiskLocations { get; set; }
        DbSet<PolicyReportingEntity> PolicyReportingEntities { get; set; }
        DbSet<PolicyServiceProvider> PolicyServiceProviders { get; set; }               
        
        DbSet<PolicySubType> PolicySubTypes { get; set; }
        DbSet<PolicyType> PolicyTypes { get; set; }
        DbSet<PolicyTypeAgentRequirement> PolicyTypeAgentRequirements { get; set; }
        DbSet<PolicyTypeEntityRequirement> PolicyTypeEntityRequirements { get; set; }
        DbSet<PolicyTypeServiceProvider> PolicyTypeServiceProviders { get; set; }

        DbSet<Public> Publics { get; set; }
        DbSet<PublicRequirement> PublicRequirements { get; set; }

        DbSet<Region> Regions { get; set; }
        DbSet<ReportingEntity> ReportingEntities { get; set; }
        DbSet<ReportingEntityBankAccount> ReportingEntityBankAccounts { get; set; }
        DbSet<ReportingEntityBranch> ReportingEntityBranches { get; set; }
        DbSet<ReportingEntityProfile> ReportingEntityProfiles { get; set; }
        
        DbSet<SequenceNumber> SequenceNumbers { get; set; }
        DbSet<ServiceProvider> ServiceProviders { get; set; }
        DbSet<ServiceProviderType> ServiceProviderTypes { get; set; }

        DbSet<TransactionTrigger> TransactionTriggers { get; set; }

        //DbSet<PolicyTransactionTrigger> PolicyTransactionTriggers { get; set; }
        //DbSet<ReportingEntityTransactionTrigger> ReportingEntityTransactionTriggers { get; set; }


        DbSet<TransactionTriggerFrequency> TransactionTriggerFrequencies { get; set; }
        DbSet<TransactionTriggerInput> TransactionTriggerInputs { get; set; }
        DbSet<TransactionTriggerStatus> TransactionTriggerStatuses { get; set; }
        DbSet<TransactionTriggerLog> TransactionTriggerLogs { get; set; }
        DbSet<TransactionTriggerException> TransactionTriggerExceptions { get; set; }


        DbSet<PolicyStatus> PolicyStatuses { get; set; }       

        
        
    }
}