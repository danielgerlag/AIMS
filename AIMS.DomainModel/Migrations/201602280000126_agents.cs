namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agents : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Agent", new[] { "ReportingEntityBranchID" });
            AddColumn("dbo.Agent", "ReportingEntityID", c => c.Int(nullable: false));
            AlterColumn("dbo.Agent", "ReportingEntityBranchID", c => c.Int());
            CreateIndex("dbo.Agent", "ReportingEntityID");
            CreateIndex("dbo.Agent", "ReportingEntityBranchID");
            AddForeignKey("dbo.Agent", "ReportingEntityID", "dbo.ReportingEntity", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agent", "ReportingEntityID", "dbo.ReportingEntity");
            DropIndex("dbo.Agent", new[] { "ReportingEntityBranchID" });
            DropIndex("dbo.Agent", new[] { "ReportingEntityID" });
            AlterColumn("dbo.Agent", "ReportingEntityBranchID", c => c.Int(nullable: false));
            DropColumn("dbo.Agent", "ReportingEntityID");
            CreateIndex("dbo.Agent", "ReportingEntityBranchID");
        }
    }
}
