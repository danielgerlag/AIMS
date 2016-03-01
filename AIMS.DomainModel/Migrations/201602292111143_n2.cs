namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JournalTemplateTxn", "ReconRequired", c => c.Boolean(nullable: false));
            DropColumn("dbo.JournalType", "ReconRequired");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JournalType", "ReconRequired", c => c.Boolean(nullable: false));
            DropColumn("dbo.JournalTemplateTxn", "ReconRequired");
        }
    }
}
