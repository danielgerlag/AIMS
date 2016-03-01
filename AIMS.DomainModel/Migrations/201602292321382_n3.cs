namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicySubType", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PolicySubType", "FullName");
        }
    }
}
