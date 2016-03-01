namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JournalTxn", "IsReconciled", c => c.Boolean(nullable: false));
            AddColumn("dbo.JournalType", "ReconRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.Public", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Public", "FullName");
            DropColumn("dbo.JournalType", "ReconRequired");
            DropColumn("dbo.JournalTxn", "IsReconciled");
        }
    }
}
