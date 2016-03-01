namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyTypeTransitionJournalTemplate", "Condition", c => c.String());
            AlterColumn("dbo.JournalTxn", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JournalTxn", "Description", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.PolicyTypeTransitionJournalTemplate", "Condition");
        }
    }
}
