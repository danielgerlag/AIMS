namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ledger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ledger",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 10),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.LedgerAccount", "LedgerID", c => c.Int());
            CreateIndex("dbo.LedgerAccount", "LedgerID");
            AddForeignKey("dbo.LedgerAccount", "LedgerID", "dbo.Ledger", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LedgerAccount", "LedgerID", "dbo.Ledger");
            DropIndex("dbo.LedgerAccount", new[] { "LedgerID" });
            DropColumn("dbo.LedgerAccount", "LedgerID");
            DropTable("dbo.Ledger");
        }
    }
}
