namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalTemplateTxn", "ReferenceInputID", "dbo.JournalTemplateInput");
            DropForeignKey("dbo.JournalTemplateInput", "JournalTemplateID", "dbo.JournalTemplate");
            DropIndex("dbo.JournalTemplateTxn", new[] { "ReferenceInputID" });
            RenameColumn(table: "dbo.Journal", name: "Agent_ID", newName: "AgentID");
            RenameColumn(table: "dbo.Journal", name: "Policy_ID", newName: "PolicyID");
            RenameColumn(table: "dbo.Journal", name: "Public_ID", newName: "PublicID");
            RenameColumn(table: "dbo.Journal", name: "ReportingEntityBranch_ID", newName: "ReportingEntityBranchID");
            RenameColumn(table: "dbo.Journal", name: "ServiceProvider_ID", newName: "ServiceProviderID");
            RenameIndex(table: "dbo.Journal", name: "IX_Policy_ID", newName: "IX_PolicyID");
            RenameIndex(table: "dbo.Journal", name: "IX_Public_ID", newName: "IX_PublicID");
            RenameIndex(table: "dbo.Journal", name: "IX_ServiceProvider_ID", newName: "IX_ServiceProviderID");
            RenameIndex(table: "dbo.Journal", name: "IX_ReportingEntityBranch_ID", newName: "IX_ReportingEntityBranchID");
            RenameIndex(table: "dbo.Journal", name: "IX_Agent_ID", newName: "IX_AgentID");
            AddColumn("dbo.JournalTemplate", "ReferenceInputID", c => c.Int());
            AddColumn("dbo.JournalTemplateInput", "JournalTemplate_ID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "AmountLedgerAccountID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "BalanceOrigin", c => c.String(nullable: false, maxLength: 1));
            CreateIndex("dbo.JournalTemplate", "ReferenceInputID");
            CreateIndex("dbo.JournalTemplateInput", "JournalTemplate_ID");
            CreateIndex("dbo.JournalTemplateTxn", "AmountLedgerAccountID");
            AddForeignKey("dbo.JournalTemplateTxn", "AmountLedgerAccountID", "dbo.LedgerAccount", "ID");
            AddForeignKey("dbo.JournalTemplate", "ReferenceInputID", "dbo.JournalTemplateInput", "ID");
            AddForeignKey("dbo.JournalTemplateInput", "JournalTemplate_ID", "dbo.JournalTemplate", "ID");
            DropColumn("dbo.JournalTemplateTxn", "ReferenceInputID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JournalTemplateTxn", "ReferenceInputID", c => c.Int());
            DropForeignKey("dbo.JournalTemplateInput", "JournalTemplate_ID", "dbo.JournalTemplate");
            DropForeignKey("dbo.JournalTemplate", "ReferenceInputID", "dbo.JournalTemplateInput");
            DropForeignKey("dbo.JournalTemplateTxn", "AmountLedgerAccountID", "dbo.LedgerAccount");
            DropIndex("dbo.JournalTemplateTxn", new[] { "AmountLedgerAccountID" });
            DropIndex("dbo.JournalTemplateInput", new[] { "JournalTemplate_ID" });
            DropIndex("dbo.JournalTemplate", new[] { "ReferenceInputID" });
            DropColumn("dbo.JournalTemplateTxn", "BalanceOrigin");
            DropColumn("dbo.JournalTemplateTxn", "AmountLedgerAccountID");
            DropColumn("dbo.JournalTemplateInput", "JournalTemplate_ID");
            DropColumn("dbo.JournalTemplate", "ReferenceInputID");
            RenameIndex(table: "dbo.Journal", name: "IX_AgentID", newName: "IX_Agent_ID");
            RenameIndex(table: "dbo.Journal", name: "IX_ReportingEntityBranchID", newName: "IX_ReportingEntityBranch_ID");
            RenameIndex(table: "dbo.Journal", name: "IX_ServiceProviderID", newName: "IX_ServiceProvider_ID");
            RenameIndex(table: "dbo.Journal", name: "IX_PublicID", newName: "IX_Public_ID");
            RenameIndex(table: "dbo.Journal", name: "IX_PolicyID", newName: "IX_Policy_ID");
            RenameColumn(table: "dbo.Journal", name: "ServiceProviderID", newName: "ServiceProvider_ID");
            RenameColumn(table: "dbo.Journal", name: "ReportingEntityBranchID", newName: "ReportingEntityBranch_ID");
            RenameColumn(table: "dbo.Journal", name: "PublicID", newName: "Public_ID");
            RenameColumn(table: "dbo.Journal", name: "PolicyID", newName: "Policy_ID");
            RenameColumn(table: "dbo.Journal", name: "AgentID", newName: "Agent_ID");
            CreateIndex("dbo.JournalTemplateTxn", "ReferenceInputID");
            AddForeignKey("dbo.JournalTemplateInput", "JournalTemplateID", "dbo.JournalTemplate", "ID");
            AddForeignKey("dbo.JournalTemplateTxn", "ReferenceInputID", "dbo.JournalTemplateInput", "ID");
        }
    }
}
