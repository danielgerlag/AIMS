namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ptt2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JournalTxn", "TransactionOrigin");
            DropColumn("dbo.TransactionTrigger", "TransactionOrigin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionTrigger", "TransactionOrigin", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.JournalTxn", "TransactionOrigin", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
