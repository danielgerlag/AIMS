namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityBranchID = c.Int(nullable: false),
                        PublicID = c.Int(nullable: false),
                        AgentTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AgentType", t => t.AgentTypeID)
                .ForeignKey("dbo.ReportingEntityBranch", t => t.ReportingEntityBranchID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .Index(t => t.ReportingEntityBranchID)
                .Index(t => t.PublicID)
                .Index(t => t.AgentTypeID);
            
            CreateTable(
                "dbo.AgentType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Journal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityID = c.Int(nullable: false),
                        JournalTypeID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        TransactionOrigin = c.String(nullable: false, maxLength: 1),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                        Public_ID = c.Int(),
                        Policy_ID = c.Int(),
                        ServiceProvider_ID = c.Int(),
                        ReportingEntityBranch_ID = c.Int(),
                        Agent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Public", t => t.Public_ID)
                .ForeignKey("dbo.Policy", t => t.Policy_ID)
                .ForeignKey("dbo.ReportingEntity", t => t.ReportingEntityID)
                .ForeignKey("dbo.ServiceProvider", t => t.ServiceProvider_ID)
                .ForeignKey("dbo.ReportingEntityBranch", t => t.ReportingEntityBranch_ID)
                .ForeignKey("dbo.JournalType", t => t.JournalTypeID)
                .ForeignKey("dbo.Agent", t => t.Agent_ID)
                .Index(t => t.ReportingEntityID)
                .Index(t => t.JournalTypeID)
                .Index(t => t.Public_ID)
                .Index(t => t.Policy_ID)
                .Index(t => t.ServiceProvider_ID)
                .Index(t => t.ReportingEntityBranch_ID)
                .Index(t => t.Agent_ID);
            
            CreateTable(
                "dbo.JournalTxn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JournalID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        TransactionOrigin = c.String(nullable: false, maxLength: 1),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PublicID = c.Int(),
                        PolicyID = c.Int(),
                        AgentID = c.Int(),
                        ReportingEntityBranchID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.AgentID)
                .ForeignKey("dbo.Journal", t => t.JournalID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .ForeignKey("dbo.ReportingEntityBranch", t => t.ReportingEntityBranchID)
                .Index(t => t.JournalID)
                .Index(t => t.PublicID)
                .Index(t => t.PolicyID)
                .Index(t => t.AgentID)
                .Index(t => t.ReportingEntityBranchID);
            
            CreateTable(
                "dbo.LedgerTxn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityID = c.Int(nullable: false),
                        JournalTxnID = c.Int(nullable: false),
                        LedgerAccountID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        TransactionOrigin = c.String(nullable: false, maxLength: 1),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PublicID = c.Int(),
                        PolicyID = c.Int(),
                        AgentID = c.Int(),
                        ReportingEntityBranchID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.AgentID)
                .ForeignKey("dbo.JournalTxn", t => t.JournalTxnID)
                .ForeignKey("dbo.LedgerAccount", t => t.LedgerAccountID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .ForeignKey("dbo.ReportingEntity", t => t.ReportingEntityID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .ForeignKey("dbo.ReportingEntityBranch", t => t.ReportingEntityBranchID)
                .Index(t => t.ReportingEntityID)
                .Index(t => t.JournalTxnID)
                .Index(t => t.LedgerAccountID)
                .Index(t => t.PublicID)
                .Index(t => t.PolicyID)
                .Index(t => t.AgentID)
                .Index(t => t.ReportingEntityBranchID);
            
            CreateTable(
                "dbo.LedgerAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityProfileID = c.Int(nullable: false),
                        LedgerAccountTypeID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LedgerAccountType", t => t.LedgerAccountTypeID)
                .ForeignKey("dbo.ReportingEntityProfile", t => t.ReportingEntityProfileID)
                .Index(t => t.ReportingEntityProfileID)
                .Index(t => t.LedgerAccountTypeID);
            
            CreateTable(
                "dbo.LedgerAccountType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsCurrent = c.Boolean(nullable: false),
                        IsAsset = c.Boolean(nullable: false),
                        IsLiability = c.Boolean(nullable: false),
                        IsIncome = c.Boolean(nullable: false),
                        IsExpense = c.Boolean(nullable: false),
                        IsDebtor = c.Boolean(nullable: false),
                        IsCredior = c.Boolean(nullable: false),
                        IsControl = c.Boolean(nullable: false),
                        IsEquity = c.Boolean(nullable: false),
                        CreditPositive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReportingEntityProfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CoverageProfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityProfileID = c.Int(nullable: false),
                        CoverageTypeID = c.Int(nullable: false),
                        IncomeLedgerAccountID = c.Int(nullable: false),
                        ExpenseLedgerAccountID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CoverageType", t => t.CoverageTypeID)
                .ForeignKey("dbo.LedgerAccount", t => t.ExpenseLedgerAccountID)
                .ForeignKey("dbo.LedgerAccount", t => t.IncomeLedgerAccountID)
                .ForeignKey("dbo.ReportingEntityProfile", t => t.ReportingEntityProfileID)
                .Index(t => t.ReportingEntityProfileID)
                .Index(t => t.CoverageTypeID)
                .Index(t => t.IncomeLedgerAccountID)
                .Index(t => t.ExpenseLedgerAccountID);
            
            CreateTable(
                "dbo.CoverageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Code = c.String(maxLength: 200),
                        RegionID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Region", t => t.RegionID)
                .Index(t => t.RegionID);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JournalTemplate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityProfileID = c.Int(nullable: false),
                        JournalTypeID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        TransactionOrigin = c.String(nullable: false, maxLength: 1),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JournalType", t => t.JournalTypeID)
                .ForeignKey("dbo.ReportingEntityProfile", t => t.ReportingEntityProfileID)
                .Index(t => t.ReportingEntityProfileID)
                .Index(t => t.JournalTypeID);
            
            CreateTable(
                "dbo.JournalTemplateInput",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JournalTemplateID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 500),
                        AttributeDataTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttributeDataType", t => t.AttributeDataTypeID)
                .ForeignKey("dbo.JournalTemplate", t => t.JournalTemplateID)
                .Index(t => t.JournalTemplateID)
                .Index(t => t.AttributeDataTypeID);
            
            CreateTable(
                "dbo.AttributeDataType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JournalTemplateTxn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JournalTemplateID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        PublicRequirementID = c.Int(),
                        ServiceProviderTypeID = c.Int(),
                        AgentTypeID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AgentType", t => t.AgentTypeID)
                .ForeignKey("dbo.JournalTemplate", t => t.JournalTemplateID)
                .ForeignKey("dbo.PublicRequirement", t => t.PublicRequirementID)
                .ForeignKey("dbo.ServiceProviderType", t => t.ServiceProviderTypeID)
                .Index(t => t.JournalTemplateID)
                .Index(t => t.PublicRequirementID)
                .Index(t => t.ServiceProviderTypeID)
                .Index(t => t.AgentTypeID);
            
            CreateTable(
                "dbo.JournalTemplateTxnPosting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JournalTemplateTxnID = c.Int(nullable: false),
                        LedgerAccountID = c.Int(nullable: false),
                        PostType = c.String(nullable: false, maxLength: 1),
                        AddBaseAmount = c.Boolean(nullable: false),
                        InheritPolicy = c.Boolean(nullable: false),
                        InheritPublic = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JournalTemplateTxn", t => t.JournalTemplateTxnID)
                .ForeignKey("dbo.LedgerAccount", t => t.LedgerAccountID)
                .Index(t => t.JournalTemplateTxnID)
                .Index(t => t.LedgerAccountID);
            
            CreateTable(
                "dbo.PublicRequirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        IsServiceProvider = c.Boolean(nullable: false),
                        IsAgent = c.Boolean(nullable: false),
                        IsPolicyHolder = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ServiceProviderType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JournalType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        IsGeneral = c.Boolean(nullable: false),
                        IsInvoice = c.Boolean(nullable: false),
                        IsPayment = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Policy",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyNumber = c.String(maxLength: 100),
                        EffectiveDate = c.DateTime(),
                        ExpiryDate = c.DateTime(),
                        PolicySubTypeID = c.Int(nullable: false),
                        BillingPublicID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Public", t => t.BillingPublicID)
                .ForeignKey("dbo.PolicySubType", t => t.PolicySubTypeID)
                .Index(t => t.PolicySubTypeID)
                .Index(t => t.BillingPublicID);
            
            CreateTable(
                "dbo.PolicyAgent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyID = c.Int(nullable: false),
                        AgentTypeID = c.Int(nullable: false),
                        AgentID = c.Int(nullable: false),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.AgentID)
                .ForeignKey("dbo.AgentType", t => t.AgentTypeID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .Index(t => t.PolicyID)
                .Index(t => t.AgentTypeID)
                .Index(t => t.AgentID);
            
            CreateTable(
                "dbo.Public",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        FirstName = c.String(maxLength: 200),
                        PartyType = c.String(nullable: false, maxLength: 1),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BankAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublicID = c.Int(nullable: false),
                        InstitutionCode = c.String(maxLength: 20),
                        BranchCode = c.String(maxLength: 50),
                        AccountNumber = c.String(maxLength: 50),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .Index(t => t.PublicID);
            
            CreateTable(
                "dbo.ContactDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublicID = c.Int(nullable: false),
                        Data = c.String(maxLength: 500),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .Index(t => t.PublicID);
            
            CreateTable(
                "dbo.PolicyCoverage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyID = c.Int(nullable: false),
                        CoverageTypeID = c.Int(nullable: false),
                        InsurableItemID = c.Int(),
                        Premium = c.Decimal(precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CoverageType", t => t.CoverageTypeID)
                .ForeignKey("dbo.InsurableItem", t => t.InsurableItemID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .Index(t => t.PolicyID)
                .Index(t => t.CoverageTypeID)
                .Index(t => t.InsurableItemID);
            
            CreateTable(
                "dbo.InsurableItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsurableItemClassID = c.Int(nullable: false),
                        PolicyRiskLocationID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                        SerialNo = c.String(maxLength: 200),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItemClass", t => t.InsurableItemClassID)
                .ForeignKey("dbo.PolicyRiskLocation", t => t.PolicyRiskLocationID)
                .Index(t => t.InsurableItemClassID)
                .Index(t => t.PolicyRiskLocationID);
            
            CreateTable(
                "dbo.InsurableItemAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsurableItemID = c.Int(nullable: false),
                        InsurableItemClassAttributeID = c.Int(nullable: false),
                        Value = c.String(maxLength: 500),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItem", t => t.InsurableItemID)
                .ForeignKey("dbo.InsurableItemClassAttribute", t => t.InsurableItemClassAttributeID)
                .Index(t => t.InsurableItemID)
                .Index(t => t.InsurableItemClassAttributeID);
            
            CreateTable(
                "dbo.InsurableItemClassAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Prompt = c.String(maxLength: 200),
                        DisplayOrder = c.Int(nullable: false),
                        AL3Group = c.String(maxLength: 50),
                        AL3Element = c.String(maxLength: 50),
                        AttributeDataTypeID = c.Int(nullable: false),
                        DecimalPlaces = c.Int(),
                        Required = c.Boolean(nullable: false),
                        IndexField = c.Boolean(nullable: false),
                        Key = c.Boolean(nullable: false),
                        InsurableItemClassAttributeGroupID = c.Int(nullable: false),
                        AttributeLookupListID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttributeDataType", t => t.AttributeDataTypeID)
                .ForeignKey("dbo.AttributeLookupList", t => t.AttributeLookupListID)
                .ForeignKey("dbo.InsurableItemClassAttributeGroup", t => t.InsurableItemClassAttributeGroupID)
                .Index(t => t.AttributeDataTypeID)
                .Index(t => t.InsurableItemClassAttributeGroupID)
                .Index(t => t.AttributeLookupListID);
            
            CreateTable(
                "dbo.AttributeLookupList",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CSIOCode = c.String(maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AttributeLookupItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        CSIOCode = c.String(maxLength: 100),
                        AttributeLookupListID = c.Int(nullable: false),
                        AttributeLookupSubListID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttributeLookupList", t => t.AttributeLookupListID)
                .ForeignKey("dbo.AttributeLookupSubList", t => t.AttributeLookupSubListID)
                .Index(t => t.AttributeLookupListID)
                .Index(t => t.AttributeLookupSubListID);
            
            CreateTable(
                "dbo.AttributeLookupSubList",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CSIOCode = c.String(maxLength: 100),
                        AttributeLookupListID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttributeLookupList", t => t.AttributeLookupListID)
                .Index(t => t.AttributeLookupListID);
            
            CreateTable(
                "dbo.InsurableItemClassAttributeGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Prompt = c.String(maxLength: 200),
                        DisplayOrder = c.Int(nullable: false),
                        InsurableItemClassID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItemClass", t => t.InsurableItemClassID)
                .Index(t => t.InsurableItemClassID);
            
            CreateTable(
                "dbo.InsurableItemClass",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ParentInsurableItemClassID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItemClass", t => t.ParentInsurableItemClassID)
                .Index(t => t.ParentInsurableItemClassID);
            
            CreateTable(
                "dbo.InsurableItemClassOperatorType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsurableItemClassID = c.Int(nullable: false),
                        OperatorTypeID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItemClass", t => t.InsurableItemClassID)
                .ForeignKey("dbo.OperatorType", t => t.OperatorTypeID)
                .Index(t => t.InsurableItemClassID)
                .Index(t => t.OperatorTypeID);
            
            CreateTable(
                "dbo.OperatorType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OperatorTypeAttributeGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Prompt = c.String(maxLength: 200),
                        DisplayOrder = c.Int(nullable: false),
                        OperatorTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OperatorType", t => t.OperatorTypeID)
                .Index(t => t.OperatorTypeID);
            
            CreateTable(
                "dbo.OperatorTypeAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Prompt = c.String(maxLength: 200),
                        DisplayOrder = c.Int(nullable: false),
                        AL3Group = c.String(maxLength: 50),
                        AL3Element = c.String(maxLength: 50),
                        AttributeDataTypeID = c.Int(nullable: false),
                        DecimalPlaces = c.Int(),
                        Required = c.Boolean(nullable: false),
                        IndexField = c.Boolean(nullable: false),
                        OperatorTypeAttributeGroupID = c.Int(nullable: false),
                        AttributeLookupListID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttributeDataType", t => t.AttributeDataTypeID)
                .ForeignKey("dbo.AttributeLookupList", t => t.AttributeLookupListID)
                .ForeignKey("dbo.OperatorTypeAttributeGroup", t => t.OperatorTypeAttributeGroupID)
                .Index(t => t.AttributeDataTypeID)
                .Index(t => t.OperatorTypeAttributeGroupID)
                .Index(t => t.AttributeLookupListID);
            
            CreateTable(
                "dbo.Operator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsurableItemID = c.Int(nullable: false),
                        OperatorPublicID = c.Int(nullable: false),
                        OperatorTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItem", t => t.InsurableItemID)
                .ForeignKey("dbo.Public", t => t.OperatorPublicID)
                .ForeignKey("dbo.OperatorType", t => t.OperatorTypeID)
                .Index(t => t.InsurableItemID)
                .Index(t => t.OperatorPublicID)
                .Index(t => t.OperatorTypeID);
            
            CreateTable(
                "dbo.OperatorAttribute",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OperatorID = c.Int(nullable: false),
                        OperatorTypeAttributeID = c.Int(nullable: false),
                        Value = c.String(maxLength: 500),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Operator", t => t.OperatorID)
                .ForeignKey("dbo.OperatorTypeAttribute", t => t.OperatorTypeAttributeID)
                .Index(t => t.OperatorID)
                .Index(t => t.OperatorTypeAttributeID);
            
            CreateTable(
                "dbo.PolicyRiskLocation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyID = c.Int(nullable: false),
                        StreetAddressLine1 = c.String(nullable: false, maxLength: 200),
                        StreetAddressLine2 = c.String(maxLength: 200),
                        City = c.String(maxLength: 200),
                        PostalCode = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .Index(t => t.PolicyID);
            
            CreateTable(
                "dbo.PolicyHolder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublicID = c.Int(nullable: false),
                        PolicyID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .Index(t => t.PublicID)
                .Index(t => t.PolicyID);
            
            CreateTable(
                "dbo.PolicySubType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        PolicyTypeID = c.Int(nullable: false),
                        SequenceNumberID = c.Int(nullable: false),
                        RegionID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .ForeignKey("dbo.Region", t => t.RegionID)
                .ForeignKey("dbo.SequenceNumber", t => t.SequenceNumberID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.SequenceNumberID)
                .Index(t => t.RegionID);
            
            CreateTable(
                "dbo.PolicyType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PolicyTypeAgentRequirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeID = c.Int(nullable: false),
                        AgentTypeID = c.Int(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AgentType", t => t.AgentTypeID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.AgentTypeID);
            
            CreateTable(
                "dbo.PolicyTypeEntityRequirement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        PolicyTypeID = c.Int(nullable: false),
                        ReportingEntityProfileID = c.Int(nullable: false),
                        DefaultReportingEntityID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ReportingEntity", t => t.DefaultReportingEntityID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .ForeignKey("dbo.ReportingEntityProfile", t => t.ReportingEntityProfileID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.ReportingEntityProfileID)
                .Index(t => t.DefaultReportingEntityID);
            
            CreateTable(
                "dbo.ReportingEntity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublicID = c.Int(nullable: false),
                        ReportingEntityProfileID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .ForeignKey("dbo.ReportingEntityProfile", t => t.ReportingEntityProfileID)
                .Index(t => t.PublicID)
                .Index(t => t.ReportingEntityProfileID);
            
            CreateTable(
                "dbo.ReportingEntityBankAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityID = c.Int(nullable: false),
                        BankAccountID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BankAccount", t => t.BankAccountID)
                .ForeignKey("dbo.ReportingEntity", t => t.ReportingEntityID)
                .Index(t => t.ReportingEntityID)
                .Index(t => t.BankAccountID);
            
            CreateTable(
                "dbo.PolicyTypeServiceProvider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeID = c.Int(nullable: false),
                        ServiceProviderTypeID = c.Int(nullable: false),
                        DefaultServiceProviderID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ServiceProvider", t => t.DefaultServiceProviderID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .ForeignKey("dbo.ServiceProviderType", t => t.ServiceProviderTypeID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.ServiceProviderTypeID)
                .Index(t => t.DefaultServiceProviderID);
            
            CreateTable(
                "dbo.ServiceProvider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublicID = c.Int(nullable: false),
                        ServiceProviderTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Public", t => t.PublicID)
                .ForeignKey("dbo.ServiceProviderType", t => t.ServiceProviderTypeID)
                .Index(t => t.PublicID)
                .Index(t => t.ServiceProviderTypeID);
            
            CreateTable(
                "dbo.SequenceNumber",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        NextValue = c.Int(nullable: false),
                        FormatMask = c.String(nullable: false, maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PolicyReportingEntity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyID = c.Int(nullable: false),
                        PolicyTypeEntityRequirementID = c.Int(nullable: false),
                        ReportingEntityID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .ForeignKey("dbo.PolicyTypeEntityRequirement", t => t.PolicyTypeEntityRequirementID)
                .ForeignKey("dbo.ReportingEntity", t => t.ReportingEntityID)
                .Index(t => t.PolicyID)
                .Index(t => t.PolicyTypeEntityRequirementID)
                .Index(t => t.ReportingEntityID);
            
            CreateTable(
                "dbo.PolicyServiceProvider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyID = c.Int(nullable: false),
                        ServiceProviderTypeID = c.Int(nullable: false),
                        ServiceProviderID = c.Int(nullable: false),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .ForeignKey("dbo.ServiceProvider", t => t.ServiceProviderID)
                .ForeignKey("dbo.ServiceProviderType", t => t.ServiceProviderTypeID)
                .Index(t => t.PolicyID)
                .Index(t => t.ServiceProviderTypeID)
                .Index(t => t.ServiceProviderID);
            
            CreateTable(
                "dbo.ReportingEntityBranch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ReportingEntity", t => t.ReportingEntityID)
                .Index(t => t.ReportingEntityID);
            
            CreateTable(
                "dbo.BusinessLine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CSIOCode = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContextParameter",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        DecimalPlaces = c.Int(nullable: false),
                        IsPercentage = c.Boolean(nullable: false),
                        Code = c.String(nullable: false),
                        UserPrompt = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JournalTxnClass",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 300),
                        IsDailyCalc = c.Boolean(nullable: false),
                        IsDefinedAmount = c.Boolean(nullable: false),
                        IsPercentage = c.Boolean(nullable: false),
                        OfLedgerAccount = c.Boolean(nullable: false),
                        OfContextParameter = c.Boolean(nullable: false),
                        OfCoveragePremium = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PolicyStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        IsQuote = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransactionTriggerFrequency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Code = c.String(nullable: false, maxLength: 10),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.TransactionTriggerInput",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionTriggerID = c.Int(nullable: false),
                        JournalTemplateInputID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JournalTemplateInput", t => t.JournalTemplateInputID)
                .ForeignKey("dbo.TransactionTrigger", t => t.TransactionTriggerID)
                .Index(t => t.TransactionTriggerID)
                .Index(t => t.JournalTemplateInputID);
            
            CreateTable(
                "dbo.TransactionTrigger",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportingEntityID = c.Int(nullable: false),
                        JournalTemplateID = c.Int(nullable: false),
                        TransactionTriggerFrequencyID = c.Int(nullable: false),
                        TransactionTriggerStatusID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        TxnDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JournalTemplate", t => t.JournalTemplateID)
                .ForeignKey("dbo.ReportingEntity", t => t.ReportingEntityID)
                .ForeignKey("dbo.TransactionTriggerFrequency", t => t.TransactionTriggerFrequencyID)
                .ForeignKey("dbo.TransactionTriggerStatus", t => t.TransactionTriggerStatusID)
                .Index(t => t.ReportingEntityID)
                .Index(t => t.JournalTemplateID)
                .Index(t => t.TransactionTriggerFrequencyID)
                .Index(t => t.TransactionTriggerStatusID);
            
            CreateTable(
                "dbo.TransactionTriggerStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Code = c.String(nullable: false, maxLength: 10),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTrigger", "TransactionTriggerStatusID", "dbo.TransactionTriggerStatus");
            DropForeignKey("dbo.TransactionTrigger", "TransactionTriggerFrequencyID", "dbo.TransactionTriggerFrequency");
            DropForeignKey("dbo.TransactionTrigger", "ReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.TransactionTrigger", "JournalTemplateID", "dbo.JournalTemplate");
            DropForeignKey("dbo.TransactionTriggerInput", "TransactionTriggerID", "dbo.TransactionTrigger");
            DropForeignKey("dbo.TransactionTriggerInput", "JournalTemplateInputID", "dbo.JournalTemplateInput");
            DropForeignKey("dbo.Agent", "PublicID", "dbo.Public");
            DropForeignKey("dbo.Journal", "Agent_ID", "dbo.Agent");
            DropForeignKey("dbo.Journal", "JournalTypeID", "dbo.JournalType");
            DropForeignKey("dbo.JournalTxn", "ReportingEntityBranchID", "dbo.ReportingEntityBranch");
            DropForeignKey("dbo.JournalTxn", "PublicID", "dbo.Public");
            DropForeignKey("dbo.JournalTxn", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.ReportingEntityBranch", "ReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.LedgerTxn", "ReportingEntityBranchID", "dbo.ReportingEntityBranch");
            DropForeignKey("dbo.Journal", "ReportingEntityBranch_ID", "dbo.ReportingEntityBranch");
            DropForeignKey("dbo.Agent", "ReportingEntityBranchID", "dbo.ReportingEntityBranch");
            DropForeignKey("dbo.LedgerTxn", "PublicID", "dbo.Public");
            DropForeignKey("dbo.PolicyServiceProvider", "ServiceProviderTypeID", "dbo.ServiceProviderType");
            DropForeignKey("dbo.PolicyServiceProvider", "ServiceProviderID", "dbo.ServiceProvider");
            DropForeignKey("dbo.PolicyServiceProvider", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.PolicyReportingEntity", "ReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.PolicyReportingEntity", "PolicyTypeEntityRequirementID", "dbo.PolicyTypeEntityRequirement");
            DropForeignKey("dbo.PolicyReportingEntity", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.Policy", "PolicySubTypeID", "dbo.PolicySubType");
            DropForeignKey("dbo.PolicySubType", "SequenceNumberID", "dbo.SequenceNumber");
            DropForeignKey("dbo.PolicySubType", "RegionID", "dbo.Region");
            DropForeignKey("dbo.PolicyTypeServiceProvider", "ServiceProviderTypeID", "dbo.ServiceProviderType");
            DropForeignKey("dbo.PolicyTypeServiceProvider", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeServiceProvider", "DefaultServiceProviderID", "dbo.ServiceProvider");
            DropForeignKey("dbo.ServiceProvider", "ServiceProviderTypeID", "dbo.ServiceProviderType");
            DropForeignKey("dbo.ServiceProvider", "PublicID", "dbo.Public");
            DropForeignKey("dbo.Journal", "ServiceProvider_ID", "dbo.ServiceProvider");
            DropForeignKey("dbo.PolicySubType", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeEntityRequirement", "ReportingEntityProfileID", "dbo.ReportingEntityProfile");
            DropForeignKey("dbo.PolicyTypeEntityRequirement", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeEntityRequirement", "DefaultReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.ReportingEntity", "ReportingEntityProfileID", "dbo.ReportingEntityProfile");
            DropForeignKey("dbo.ReportingEntityBankAccount", "ReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.ReportingEntityBankAccount", "BankAccountID", "dbo.BankAccount");
            DropForeignKey("dbo.ReportingEntity", "PublicID", "dbo.Public");
            DropForeignKey("dbo.LedgerTxn", "ReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.Journal", "ReportingEntityID", "dbo.ReportingEntity");
            DropForeignKey("dbo.PolicyTypeAgentRequirement", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeAgentRequirement", "AgentTypeID", "dbo.AgentType");
            DropForeignKey("dbo.PolicyHolder", "PublicID", "dbo.Public");
            DropForeignKey("dbo.PolicyHolder", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.LedgerTxn", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.Journal", "Policy_ID", "dbo.Policy");
            DropForeignKey("dbo.PolicyCoverage", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.PolicyRiskLocation", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.InsurableItem", "PolicyRiskLocationID", "dbo.PolicyRiskLocation");
            DropForeignKey("dbo.PolicyCoverage", "InsurableItemID", "dbo.InsurableItem");
            DropForeignKey("dbo.Operator", "OperatorTypeID", "dbo.OperatorType");
            DropForeignKey("dbo.Operator", "OperatorPublicID", "dbo.Public");
            DropForeignKey("dbo.Operator", "InsurableItemID", "dbo.InsurableItem");
            DropForeignKey("dbo.OperatorAttribute", "OperatorTypeAttributeID", "dbo.OperatorTypeAttribute");
            DropForeignKey("dbo.OperatorAttribute", "OperatorID", "dbo.Operator");
            DropForeignKey("dbo.InsurableItem", "InsurableItemClassID", "dbo.InsurableItemClass");
            DropForeignKey("dbo.InsurableItemAttribute", "InsurableItemClassAttributeID", "dbo.InsurableItemClassAttribute");
            DropForeignKey("dbo.InsurableItemClass", "ParentInsurableItemClassID", "dbo.InsurableItemClass");
            DropForeignKey("dbo.InsurableItemClassOperatorType", "OperatorTypeID", "dbo.OperatorType");
            DropForeignKey("dbo.OperatorTypeAttributeGroup", "OperatorTypeID", "dbo.OperatorType");
            DropForeignKey("dbo.OperatorTypeAttribute", "OperatorTypeAttributeGroupID", "dbo.OperatorTypeAttributeGroup");
            DropForeignKey("dbo.OperatorTypeAttribute", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropForeignKey("dbo.OperatorTypeAttribute", "AttributeDataTypeID", "dbo.AttributeDataType");
            DropForeignKey("dbo.InsurableItemClassOperatorType", "InsurableItemClassID", "dbo.InsurableItemClass");
            DropForeignKey("dbo.InsurableItemClassAttributeGroup", "InsurableItemClassID", "dbo.InsurableItemClass");
            DropForeignKey("dbo.InsurableItemClassAttribute", "InsurableItemClassAttributeGroupID", "dbo.InsurableItemClassAttributeGroup");
            DropForeignKey("dbo.InsurableItemClassAttribute", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropForeignKey("dbo.AttributeLookupItem", "AttributeLookupSubListID", "dbo.AttributeLookupSubList");
            DropForeignKey("dbo.AttributeLookupSubList", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropForeignKey("dbo.AttributeLookupItem", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropForeignKey("dbo.InsurableItemClassAttribute", "AttributeDataTypeID", "dbo.AttributeDataType");
            DropForeignKey("dbo.InsurableItemAttribute", "InsurableItemID", "dbo.InsurableItem");
            DropForeignKey("dbo.PolicyCoverage", "CoverageTypeID", "dbo.CoverageType");
            DropForeignKey("dbo.Policy", "BillingPublicID", "dbo.Public");
            DropForeignKey("dbo.Journal", "Public_ID", "dbo.Public");
            DropForeignKey("dbo.ContactDetail", "PublicID", "dbo.Public");
            DropForeignKey("dbo.BankAccount", "PublicID", "dbo.Public");
            DropForeignKey("dbo.PolicyAgent", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.PolicyAgent", "AgentTypeID", "dbo.AgentType");
            DropForeignKey("dbo.PolicyAgent", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.LedgerTxn", "LedgerAccountID", "dbo.LedgerAccount");
            DropForeignKey("dbo.LedgerAccount", "ReportingEntityProfileID", "dbo.ReportingEntityProfile");
            DropForeignKey("dbo.JournalTemplate", "ReportingEntityProfileID", "dbo.ReportingEntityProfile");
            DropForeignKey("dbo.JournalTemplate", "JournalTypeID", "dbo.JournalType");
            DropForeignKey("dbo.JournalTemplateTxn", "ServiceProviderTypeID", "dbo.ServiceProviderType");
            DropForeignKey("dbo.JournalTemplateTxn", "PublicRequirementID", "dbo.PublicRequirement");
            DropForeignKey("dbo.JournalTemplateTxnPosting", "LedgerAccountID", "dbo.LedgerAccount");
            DropForeignKey("dbo.JournalTemplateTxnPosting", "JournalTemplateTxnID", "dbo.JournalTemplateTxn");
            DropForeignKey("dbo.JournalTemplateTxn", "JournalTemplateID", "dbo.JournalTemplate");
            DropForeignKey("dbo.JournalTemplateTxn", "AgentTypeID", "dbo.AgentType");
            DropForeignKey("dbo.JournalTemplateInput", "JournalTemplateID", "dbo.JournalTemplate");
            DropForeignKey("dbo.JournalTemplateInput", "AttributeDataTypeID", "dbo.AttributeDataType");
            DropForeignKey("dbo.CoverageProfile", "ReportingEntityProfileID", "dbo.ReportingEntityProfile");
            DropForeignKey("dbo.CoverageProfile", "IncomeLedgerAccountID", "dbo.LedgerAccount");
            DropForeignKey("dbo.CoverageProfile", "ExpenseLedgerAccountID", "dbo.LedgerAccount");
            DropForeignKey("dbo.CoverageProfile", "CoverageTypeID", "dbo.CoverageType");
            DropForeignKey("dbo.CoverageType", "RegionID", "dbo.Region");
            DropForeignKey("dbo.LedgerAccount", "LedgerAccountTypeID", "dbo.LedgerAccountType");
            DropForeignKey("dbo.LedgerTxn", "JournalTxnID", "dbo.JournalTxn");
            DropForeignKey("dbo.LedgerTxn", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.JournalTxn", "JournalID", "dbo.Journal");
            DropForeignKey("dbo.JournalTxn", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Agent", "AgentTypeID", "dbo.AgentType");
            DropIndex("dbo.TransactionTrigger", new[] { "TransactionTriggerStatusID" });
            DropIndex("dbo.TransactionTrigger", new[] { "TransactionTriggerFrequencyID" });
            DropIndex("dbo.TransactionTrigger", new[] { "JournalTemplateID" });
            DropIndex("dbo.TransactionTrigger", new[] { "ReportingEntityID" });
            DropIndex("dbo.TransactionTriggerInput", new[] { "JournalTemplateInputID" });
            DropIndex("dbo.TransactionTriggerInput", new[] { "TransactionTriggerID" });
            DropIndex("dbo.TransactionTriggerFrequency", new[] { "Code" });
            DropIndex("dbo.ReportingEntityBranch", new[] { "ReportingEntityID" });
            DropIndex("dbo.PolicyServiceProvider", new[] { "ServiceProviderID" });
            DropIndex("dbo.PolicyServiceProvider", new[] { "ServiceProviderTypeID" });
            DropIndex("dbo.PolicyServiceProvider", new[] { "PolicyID" });
            DropIndex("dbo.PolicyReportingEntity", new[] { "ReportingEntityID" });
            DropIndex("dbo.PolicyReportingEntity", new[] { "PolicyTypeEntityRequirementID" });
            DropIndex("dbo.PolicyReportingEntity", new[] { "PolicyID" });
            DropIndex("dbo.ServiceProvider", new[] { "ServiceProviderTypeID" });
            DropIndex("dbo.ServiceProvider", new[] { "PublicID" });
            DropIndex("dbo.PolicyTypeServiceProvider", new[] { "DefaultServiceProviderID" });
            DropIndex("dbo.PolicyTypeServiceProvider", new[] { "ServiceProviderTypeID" });
            DropIndex("dbo.PolicyTypeServiceProvider", new[] { "PolicyTypeID" });
            DropIndex("dbo.ReportingEntityBankAccount", new[] { "BankAccountID" });
            DropIndex("dbo.ReportingEntityBankAccount", new[] { "ReportingEntityID" });
            DropIndex("dbo.ReportingEntity", new[] { "ReportingEntityProfileID" });
            DropIndex("dbo.ReportingEntity", new[] { "PublicID" });
            DropIndex("dbo.PolicyTypeEntityRequirement", new[] { "DefaultReportingEntityID" });
            DropIndex("dbo.PolicyTypeEntityRequirement", new[] { "ReportingEntityProfileID" });
            DropIndex("dbo.PolicyTypeEntityRequirement", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicyTypeAgentRequirement", new[] { "AgentTypeID" });
            DropIndex("dbo.PolicyTypeAgentRequirement", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicySubType", new[] { "RegionID" });
            DropIndex("dbo.PolicySubType", new[] { "SequenceNumberID" });
            DropIndex("dbo.PolicySubType", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicyHolder", new[] { "PolicyID" });
            DropIndex("dbo.PolicyHolder", new[] { "PublicID" });
            DropIndex("dbo.PolicyRiskLocation", new[] { "PolicyID" });
            DropIndex("dbo.OperatorAttribute", new[] { "OperatorTypeAttributeID" });
            DropIndex("dbo.OperatorAttribute", new[] { "OperatorID" });
            DropIndex("dbo.Operator", new[] { "OperatorTypeID" });
            DropIndex("dbo.Operator", new[] { "OperatorPublicID" });
            DropIndex("dbo.Operator", new[] { "InsurableItemID" });
            DropIndex("dbo.OperatorTypeAttribute", new[] { "AttributeLookupListID" });
            DropIndex("dbo.OperatorTypeAttribute", new[] { "OperatorTypeAttributeGroupID" });
            DropIndex("dbo.OperatorTypeAttribute", new[] { "AttributeDataTypeID" });
            DropIndex("dbo.OperatorTypeAttributeGroup", new[] { "OperatorTypeID" });
            DropIndex("dbo.InsurableItemClassOperatorType", new[] { "OperatorTypeID" });
            DropIndex("dbo.InsurableItemClassOperatorType", new[] { "InsurableItemClassID" });
            DropIndex("dbo.InsurableItemClass", new[] { "ParentInsurableItemClassID" });
            DropIndex("dbo.InsurableItemClassAttributeGroup", new[] { "InsurableItemClassID" });
            DropIndex("dbo.AttributeLookupSubList", new[] { "AttributeLookupListID" });
            DropIndex("dbo.AttributeLookupItem", new[] { "AttributeLookupSubListID" });
            DropIndex("dbo.AttributeLookupItem", new[] { "AttributeLookupListID" });
            DropIndex("dbo.InsurableItemClassAttribute", new[] { "AttributeLookupListID" });
            DropIndex("dbo.InsurableItemClassAttribute", new[] { "InsurableItemClassAttributeGroupID" });
            DropIndex("dbo.InsurableItemClassAttribute", new[] { "AttributeDataTypeID" });
            DropIndex("dbo.InsurableItemAttribute", new[] { "InsurableItemClassAttributeID" });
            DropIndex("dbo.InsurableItemAttribute", new[] { "InsurableItemID" });
            DropIndex("dbo.InsurableItem", new[] { "PolicyRiskLocationID" });
            DropIndex("dbo.InsurableItem", new[] { "InsurableItemClassID" });
            DropIndex("dbo.PolicyCoverage", new[] { "InsurableItemID" });
            DropIndex("dbo.PolicyCoverage", new[] { "CoverageTypeID" });
            DropIndex("dbo.PolicyCoverage", new[] { "PolicyID" });
            DropIndex("dbo.ContactDetail", new[] { "PublicID" });
            DropIndex("dbo.BankAccount", new[] { "PublicID" });
            DropIndex("dbo.PolicyAgent", new[] { "AgentID" });
            DropIndex("dbo.PolicyAgent", new[] { "AgentTypeID" });
            DropIndex("dbo.PolicyAgent", new[] { "PolicyID" });
            DropIndex("dbo.Policy", new[] { "BillingPublicID" });
            DropIndex("dbo.Policy", new[] { "PolicySubTypeID" });
            DropIndex("dbo.JournalTemplateTxnPosting", new[] { "LedgerAccountID" });
            DropIndex("dbo.JournalTemplateTxnPosting", new[] { "JournalTemplateTxnID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "AgentTypeID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "ServiceProviderTypeID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "PublicRequirementID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "JournalTemplateID" });
            DropIndex("dbo.JournalTemplateInput", new[] { "AttributeDataTypeID" });
            DropIndex("dbo.JournalTemplateInput", new[] { "JournalTemplateID" });
            DropIndex("dbo.JournalTemplate", new[] { "JournalTypeID" });
            DropIndex("dbo.JournalTemplate", new[] { "ReportingEntityProfileID" });
            DropIndex("dbo.CoverageType", new[] { "RegionID" });
            DropIndex("dbo.CoverageProfile", new[] { "ExpenseLedgerAccountID" });
            DropIndex("dbo.CoverageProfile", new[] { "IncomeLedgerAccountID" });
            DropIndex("dbo.CoverageProfile", new[] { "CoverageTypeID" });
            DropIndex("dbo.CoverageProfile", new[] { "ReportingEntityProfileID" });
            DropIndex("dbo.LedgerAccount", new[] { "LedgerAccountTypeID" });
            DropIndex("dbo.LedgerAccount", new[] { "ReportingEntityProfileID" });
            DropIndex("dbo.LedgerTxn", new[] { "ReportingEntityBranchID" });
            DropIndex("dbo.LedgerTxn", new[] { "AgentID" });
            DropIndex("dbo.LedgerTxn", new[] { "PolicyID" });
            DropIndex("dbo.LedgerTxn", new[] { "PublicID" });
            DropIndex("dbo.LedgerTxn", new[] { "LedgerAccountID" });
            DropIndex("dbo.LedgerTxn", new[] { "JournalTxnID" });
            DropIndex("dbo.LedgerTxn", new[] { "ReportingEntityID" });
            DropIndex("dbo.JournalTxn", new[] { "ReportingEntityBranchID" });
            DropIndex("dbo.JournalTxn", new[] { "AgentID" });
            DropIndex("dbo.JournalTxn", new[] { "PolicyID" });
            DropIndex("dbo.JournalTxn", new[] { "PublicID" });
            DropIndex("dbo.JournalTxn", new[] { "JournalID" });
            DropIndex("dbo.Journal", new[] { "Agent_ID" });
            DropIndex("dbo.Journal", new[] { "ReportingEntityBranch_ID" });
            DropIndex("dbo.Journal", new[] { "ServiceProvider_ID" });
            DropIndex("dbo.Journal", new[] { "Policy_ID" });
            DropIndex("dbo.Journal", new[] { "Public_ID" });
            DropIndex("dbo.Journal", new[] { "JournalTypeID" });
            DropIndex("dbo.Journal", new[] { "ReportingEntityID" });
            DropIndex("dbo.Agent", new[] { "AgentTypeID" });
            DropIndex("dbo.Agent", new[] { "PublicID" });
            DropIndex("dbo.Agent", new[] { "ReportingEntityBranchID" });
            DropTable("dbo.TransactionTriggerStatus");
            DropTable("dbo.TransactionTrigger");
            DropTable("dbo.TransactionTriggerInput");
            DropTable("dbo.TransactionTriggerFrequency");
            DropTable("dbo.PolicyStatus");
            DropTable("dbo.JournalTxnClass");
            DropTable("dbo.ContextParameter");
            DropTable("dbo.BusinessLine");
            DropTable("dbo.ReportingEntityBranch");
            DropTable("dbo.PolicyServiceProvider");
            DropTable("dbo.PolicyReportingEntity");
            DropTable("dbo.SequenceNumber");
            DropTable("dbo.ServiceProvider");
            DropTable("dbo.PolicyTypeServiceProvider");
            DropTable("dbo.ReportingEntityBankAccount");
            DropTable("dbo.ReportingEntity");
            DropTable("dbo.PolicyTypeEntityRequirement");
            DropTable("dbo.PolicyTypeAgentRequirement");
            DropTable("dbo.PolicyType");
            DropTable("dbo.PolicySubType");
            DropTable("dbo.PolicyHolder");
            DropTable("dbo.PolicyRiskLocation");
            DropTable("dbo.OperatorAttribute");
            DropTable("dbo.Operator");
            DropTable("dbo.OperatorTypeAttribute");
            DropTable("dbo.OperatorTypeAttributeGroup");
            DropTable("dbo.OperatorType");
            DropTable("dbo.InsurableItemClassOperatorType");
            DropTable("dbo.InsurableItemClass");
            DropTable("dbo.InsurableItemClassAttributeGroup");
            DropTable("dbo.AttributeLookupSubList");
            DropTable("dbo.AttributeLookupItem");
            DropTable("dbo.AttributeLookupList");
            DropTable("dbo.InsurableItemClassAttribute");
            DropTable("dbo.InsurableItemAttribute");
            DropTable("dbo.InsurableItem");
            DropTable("dbo.PolicyCoverage");
            DropTable("dbo.ContactDetail");
            DropTable("dbo.BankAccount");
            DropTable("dbo.Public");
            DropTable("dbo.PolicyAgent");
            DropTable("dbo.Policy");
            DropTable("dbo.JournalType");
            DropTable("dbo.ServiceProviderType");
            DropTable("dbo.PublicRequirement");
            DropTable("dbo.JournalTemplateTxnPosting");
            DropTable("dbo.JournalTemplateTxn");
            DropTable("dbo.AttributeDataType");
            DropTable("dbo.JournalTemplateInput");
            DropTable("dbo.JournalTemplate");
            DropTable("dbo.Region");
            DropTable("dbo.CoverageType");
            DropTable("dbo.CoverageProfile");
            DropTable("dbo.ReportingEntityProfile");
            DropTable("dbo.LedgerAccountType");
            DropTable("dbo.LedgerAccount");
            DropTable("dbo.LedgerTxn");
            DropTable("dbo.JournalTxn");
            DropTable("dbo.Journal");
            DropTable("dbo.AgentType");
            DropTable("dbo.Agent");
        }
    }
}
