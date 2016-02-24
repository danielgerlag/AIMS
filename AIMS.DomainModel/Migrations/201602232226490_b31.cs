namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalTemplateTxn", "AgentTypeID", "dbo.AgentType");
            DropForeignKey("dbo.JournalTemplateTxn", "PublicRequirementID", "dbo.PublicRequirement");
            DropForeignKey("dbo.JournalTemplateTxn", "ServiceProviderTypeID", "dbo.ServiceProviderType");
            DropIndex("dbo.JournalTemplateTxn", new[] { "PublicRequirementID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "ServiceProviderTypeID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "AgentTypeID" });
            AddColumn("dbo.Journal", "TxnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.JournalTemplate", "PublicRequirementID", c => c.Int());
            AddColumn("dbo.JournalTemplate", "ServiceProviderTypeID", c => c.Int());
            AddColumn("dbo.JournalTemplate", "AgentTypeID", c => c.Int());
            AddColumn("dbo.PolicyHolder", "BillingPercent", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.JournalTemplate", "PublicRequirementID");
            CreateIndex("dbo.JournalTemplate", "ServiceProviderTypeID");
            CreateIndex("dbo.JournalTemplate", "AgentTypeID");
            AddForeignKey("dbo.JournalTemplate", "AgentTypeID", "dbo.AgentType", "ID");
            AddForeignKey("dbo.JournalTemplate", "PublicRequirementID", "dbo.PublicRequirement", "ID");
            AddForeignKey("dbo.JournalTemplate", "ServiceProviderTypeID", "dbo.ServiceProviderType", "ID");
            DropColumn("dbo.JournalTemplateTxn", "PublicRequirementID");
            DropColumn("dbo.JournalTemplateTxn", "ServiceProviderTypeID");
            DropColumn("dbo.JournalTemplateTxn", "AgentTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JournalTemplateTxn", "AgentTypeID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "ServiceProviderTypeID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "PublicRequirementID", c => c.Int());
            DropForeignKey("dbo.JournalTemplate", "ServiceProviderTypeID", "dbo.ServiceProviderType");
            DropForeignKey("dbo.JournalTemplate", "PublicRequirementID", "dbo.PublicRequirement");
            DropForeignKey("dbo.JournalTemplate", "AgentTypeID", "dbo.AgentType");
            DropIndex("dbo.JournalTemplate", new[] { "AgentTypeID" });
            DropIndex("dbo.JournalTemplate", new[] { "ServiceProviderTypeID" });
            DropIndex("dbo.JournalTemplate", new[] { "PublicRequirementID" });
            DropColumn("dbo.PolicyHolder", "BillingPercent");
            DropColumn("dbo.JournalTemplate", "AgentTypeID");
            DropColumn("dbo.JournalTemplate", "ServiceProviderTypeID");
            DropColumn("dbo.JournalTemplate", "PublicRequirementID");
            DropColumn("dbo.Journal", "TxnDate");
            CreateIndex("dbo.JournalTemplateTxn", "AgentTypeID");
            CreateIndex("dbo.JournalTemplateTxn", "ServiceProviderTypeID");
            CreateIndex("dbo.JournalTemplateTxn", "PublicRequirementID");
            AddForeignKey("dbo.JournalTemplateTxn", "ServiceProviderTypeID", "dbo.ServiceProviderType", "ID");
            AddForeignKey("dbo.JournalTemplateTxn", "PublicRequirementID", "dbo.PublicRequirement", "ID");
            AddForeignKey("dbo.JournalTemplateTxn", "AgentTypeID", "dbo.AgentType", "ID");
        }
    }
}
