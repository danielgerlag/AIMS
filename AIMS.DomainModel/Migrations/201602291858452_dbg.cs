namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LedgerTxn", "AbsAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LedgerTxn", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LedgerTxn", "Description");
            DropColumn("dbo.LedgerTxn", "AbsAmount");
        }
    }
}
