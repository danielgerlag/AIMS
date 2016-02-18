namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InsurableItemClassAttribute", "ShowInList", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InsurableItemClassAttribute", "ShowInList");
        }
    }
}
