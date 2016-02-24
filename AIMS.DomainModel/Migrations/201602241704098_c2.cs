namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Journal", "TransactionOrigin");
            DropColumn("dbo.LedgerTxn", "TransactionOrigin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LedgerTxn", "TransactionOrigin", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.Journal", "TransactionOrigin", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
