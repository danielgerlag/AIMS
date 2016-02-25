namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolicyTypeStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        AllowRating = c.Boolean(nullable: false),
                        AllowChanges = c.Boolean(nullable: false),
                        AllowTransactions = c.Boolean(nullable: false),
                        PolicyTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                        PolicyType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyType_ID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.PolicyType_ID);
            
            CreateTable(
                "dbo.PolicyTypeTransition",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ButtonText = c.String(maxLength: 200),
                        PolicyTypeID = c.Int(nullable: false),
                        InitialStatusID = c.Int(),
                        TargetStatusID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PolicyTypeStatus", t => t.InitialStatusID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .ForeignKey("dbo.PolicyTypeStatus", t => t.TargetStatusID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.InitialStatusID)
                .Index(t => t.TargetStatusID);
            
            CreateTable(
                "dbo.PolicyTypeTransitionInput",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeTransitionID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Prompt = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 500),
                        AttributeDataTypeID = c.Int(nullable: false),
                        Required = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttributeDataType", t => t.AttributeDataTypeID)
                .ForeignKey("dbo.PolicyTypeTransition", t => t.PolicyTypeTransitionID)
                .Index(t => t.PolicyTypeTransitionID)
                .Index(t => t.AttributeDataTypeID);
            
            CreateTable(
                "dbo.PolicyTypeTransitionJournalTemplate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeTransitionID = c.Int(nullable: false),
                        EntityRequirementID = c.Int(nullable: false),
                        JournalTemplateID = c.Int(nullable: false),
                        Script = c.String(),
                        TxnDateInputID = c.Int(),
                        TransactionTriggerStatusID = c.Int(),
                        TransactionTriggerFrequencyID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PolicyTypeEntityRequirement", t => t.EntityRequirementID)
                .ForeignKey("dbo.JournalTemplate", t => t.JournalTemplateID)
                .ForeignKey("dbo.PolicyTypeTransition", t => t.PolicyTypeTransitionID)
                .ForeignKey("dbo.TransactionTriggerFrequency", t => t.TransactionTriggerFrequencyID)
                .ForeignKey("dbo.TransactionTriggerStatus", t => t.TransactionTriggerStatusID)
                .ForeignKey("dbo.PolicyTypeTransitionInput", t => t.TxnDateInputID)
                .Index(t => t.PolicyTypeTransitionID)
                .Index(t => t.EntityRequirementID)
                .Index(t => t.JournalTemplateID)
                .Index(t => t.TxnDateInputID)
                .Index(t => t.TransactionTriggerStatusID)
                .Index(t => t.TransactionTriggerFrequencyID);
            
            CreateTable(
                "dbo.PolicyTypeTransitionJournalTemplateInput",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeTransitionJournalTemplateID = c.Int(nullable: false),
                        JournalTemplateInputID = c.Int(nullable: false),
                        PolicyTypeTransitionInputID = c.Int(),
                        Expression = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JournalTemplateInput", t => t.JournalTemplateInputID)
                .ForeignKey("dbo.PolicyTypeTransitionInput", t => t.PolicyTypeTransitionInputID)
                .ForeignKey("dbo.PolicyTypeTransitionJournalTemplate", t => t.PolicyTypeTransitionJournalTemplateID)
                .Index(t => t.PolicyTypeTransitionJournalTemplateID)
                .Index(t => t.JournalTemplateInputID)
                .Index(t => t.PolicyTypeTransitionInputID);
            
            AddColumn("dbo.Policy", "StatusID", c => c.Int());
            AddColumn("dbo.PolicyType", "InitialStatusID", c => c.Int());
            CreateIndex("dbo.Policy", "StatusID");
            CreateIndex("dbo.PolicyType", "InitialStatusID");
            AddForeignKey("dbo.PolicyType", "InitialStatusID", "dbo.PolicyTypeStatus", "ID");
            AddForeignKey("dbo.Policy", "StatusID", "dbo.PolicyTypeStatus", "ID");
            DropTable("dbo.PolicyStatus");
        }
        
        public override void Down()
        {
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
            
            DropForeignKey("dbo.Policy", "StatusID", "dbo.PolicyTypeStatus");
            DropForeignKey("dbo.PolicyTypeTransition", "TargetStatusID", "dbo.PolicyTypeStatus");
            DropForeignKey("dbo.PolicyTypeTransition", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplate", "TxnDateInputID", "dbo.PolicyTypeTransitionInput");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplate", "TransactionTriggerStatusID", "dbo.TransactionTriggerStatus");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplate", "TransactionTriggerFrequencyID", "dbo.TransactionTriggerFrequency");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplate", "PolicyTypeTransitionID", "dbo.PolicyTypeTransition");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplate", "JournalTemplateID", "dbo.JournalTemplate");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "PolicyTypeTransitionJournalTemplateID", "dbo.PolicyTypeTransitionJournalTemplate");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "PolicyTypeTransitionInputID", "dbo.PolicyTypeTransitionInput");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "JournalTemplateInputID", "dbo.JournalTemplateInput");
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplate", "EntityRequirementID", "dbo.PolicyTypeEntityRequirement");
            DropForeignKey("dbo.PolicyTypeTransitionInput", "PolicyTypeTransitionID", "dbo.PolicyTypeTransition");
            DropForeignKey("dbo.PolicyTypeTransitionInput", "AttributeDataTypeID", "dbo.AttributeDataType");
            DropForeignKey("dbo.PolicyTypeTransition", "InitialStatusID", "dbo.PolicyTypeStatus");
            DropForeignKey("dbo.PolicyTypeStatus", "PolicyType_ID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyType", "InitialStatusID", "dbo.PolicyTypeStatus");
            DropForeignKey("dbo.PolicyTypeStatus", "PolicyTypeID", "dbo.PolicyType");
            DropIndex("dbo.PolicyTypeTransitionJournalTemplateInput", new[] { "PolicyTypeTransitionInputID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplateInput", new[] { "JournalTemplateInputID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplateInput", new[] { "PolicyTypeTransitionJournalTemplateID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplate", new[] { "TransactionTriggerFrequencyID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplate", new[] { "TransactionTriggerStatusID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplate", new[] { "TxnDateInputID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplate", new[] { "JournalTemplateID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplate", new[] { "EntityRequirementID" });
            DropIndex("dbo.PolicyTypeTransitionJournalTemplate", new[] { "PolicyTypeTransitionID" });
            DropIndex("dbo.PolicyTypeTransitionInput", new[] { "AttributeDataTypeID" });
            DropIndex("dbo.PolicyTypeTransitionInput", new[] { "PolicyTypeTransitionID" });
            DropIndex("dbo.PolicyTypeTransition", new[] { "TargetStatusID" });
            DropIndex("dbo.PolicyTypeTransition", new[] { "InitialStatusID" });
            DropIndex("dbo.PolicyTypeTransition", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicyTypeStatus", new[] { "PolicyType_ID" });
            DropIndex("dbo.PolicyTypeStatus", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicyType", new[] { "InitialStatusID" });
            DropIndex("dbo.Policy", new[] { "StatusID" });
            DropColumn("dbo.PolicyType", "InitialStatusID");
            DropColumn("dbo.Policy", "StatusID");
            DropTable("dbo.PolicyTypeTransitionJournalTemplateInput");
            DropTable("dbo.PolicyTypeTransitionJournalTemplate");
            DropTable("dbo.PolicyTypeTransitionInput");
            DropTable("dbo.PolicyTypeTransition");
            DropTable("dbo.PolicyTypeStatus");
        }
    }
}
