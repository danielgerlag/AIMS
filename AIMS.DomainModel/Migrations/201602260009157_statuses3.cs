namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuses3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyTypeTransition", "Script", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PolicyTypeTransition", "Script");
        }
    }
}
