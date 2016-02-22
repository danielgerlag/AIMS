namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ledger2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LedgerAccount", new[] { "LedgerID" });
            AlterColumn("dbo.LedgerAccount", "LedgerID", c => c.Int(nullable: false));
            CreateIndex("dbo.LedgerAccount", "LedgerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LedgerAccount", new[] { "LedgerID" });
            AlterColumn("dbo.LedgerAccount", "LedgerID", c => c.Int());
            CreateIndex("dbo.LedgerAccount", "LedgerID");
        }
    }
}
