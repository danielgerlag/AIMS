namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ptt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionTrigger", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.TransactionTrigger", "ReportingEntity_ID", c => c.Int());
            CreateIndex("dbo.TransactionTrigger", "ReportingEntity_ID");
            AddForeignKey("dbo.TransactionTrigger", "ReportingEntity_ID", "dbo.ReportingEntity", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTrigger", "ReportingEntity_ID", "dbo.ReportingEntity");
            DropIndex("dbo.TransactionTrigger", new[] { "ReportingEntity_ID" });
            DropColumn("dbo.TransactionTrigger", "ReportingEntity_ID");
            DropColumn("dbo.TransactionTrigger", "Discriminator");
        }
    }
}
