namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuses4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyTypeTransitionJournalTemplate", "Description", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PolicyTypeTransitionJournalTemplate", "Description");
        }
    }
}
