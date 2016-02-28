namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agent3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agent", "ReportingEntityID", "dbo.ReportingEntity");
            DropIndex("dbo.Agent", new[] { "ReportingEntityID" });
            DropColumn("dbo.Agent", "ReportingEntityID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agent", "ReportingEntityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Agent", "ReportingEntityID");
            AddForeignKey("dbo.Agent", "ReportingEntityID", "dbo.ReportingEntity", "ID");
        }
    }
}
