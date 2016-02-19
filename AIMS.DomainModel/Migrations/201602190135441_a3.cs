namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionTrigger", "TransactionOrigin", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.TransactionTrigger", "PolicyID", c => c.Int());
            AddColumn("dbo.TransactionTrigger", "PublicID", c => c.Int());
            AddColumn("dbo.TransactionTrigger", "ServiceProviderID", c => c.Int());
            AddColumn("dbo.TransactionTrigger", "AgentID", c => c.Int());
            AddColumn("dbo.TransactionTrigger", "ReportingEntityBranchID", c => c.Int());
            CreateIndex("dbo.TransactionTrigger", "PolicyID");
            CreateIndex("dbo.TransactionTrigger", "PublicID");
            CreateIndex("dbo.TransactionTrigger", "ServiceProviderID");
            CreateIndex("dbo.TransactionTrigger", "AgentID");
            CreateIndex("dbo.TransactionTrigger", "ReportingEntityBranchID");
            AddForeignKey("dbo.TransactionTrigger", "AgentID", "dbo.Agent", "ID");
            AddForeignKey("dbo.TransactionTrigger", "PolicyID", "dbo.Policy", "ID");
            AddForeignKey("dbo.TransactionTrigger", "PublicID", "dbo.Public", "ID");
            AddForeignKey("dbo.TransactionTrigger", "ReportingEntityBranchID", "dbo.ReportingEntityBranch", "ID");
            AddForeignKey("dbo.TransactionTrigger", "ServiceProviderID", "dbo.ServiceProvider", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTrigger", "ServiceProviderID", "dbo.ServiceProvider");
            DropForeignKey("dbo.TransactionTrigger", "ReportingEntityBranchID", "dbo.ReportingEntityBranch");
            DropForeignKey("dbo.TransactionTrigger", "PublicID", "dbo.Public");
            DropForeignKey("dbo.TransactionTrigger", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.TransactionTrigger", "AgentID", "dbo.Agent");
            DropIndex("dbo.TransactionTrigger", new[] { "ReportingEntityBranchID" });
            DropIndex("dbo.TransactionTrigger", new[] { "AgentID" });
            DropIndex("dbo.TransactionTrigger", new[] { "ServiceProviderID" });
            DropIndex("dbo.TransactionTrigger", new[] { "PublicID" });
            DropIndex("dbo.TransactionTrigger", new[] { "PolicyID" });
            DropColumn("dbo.TransactionTrigger", "ReportingEntityBranchID");
            DropColumn("dbo.TransactionTrigger", "AgentID");
            DropColumn("dbo.TransactionTrigger", "ServiceProviderID");
            DropColumn("dbo.TransactionTrigger", "PublicID");
            DropColumn("dbo.TransactionTrigger", "PolicyID");
            DropColumn("dbo.TransactionTrigger", "TransactionOrigin");
        }
    }
}
