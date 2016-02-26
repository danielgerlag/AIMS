namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuses5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyTypeTransition", "IntroText", c => c.String());
            AddColumn("dbo.PolicyTypeTransition", "SummaryText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PolicyTypeTransition", "SummaryText");
            DropColumn("dbo.PolicyTypeTransition", "IntroText");
        }
    }
}
