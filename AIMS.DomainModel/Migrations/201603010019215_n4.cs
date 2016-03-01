namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JournalTemplateTxn", "InvertPercentage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JournalTemplateTxn", "InvertPercentage");
        }
    }
}
