namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InsurableItem", "Description");
            DropColumn("dbo.InsurableItem", "SerialNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InsurableItem", "SerialNo", c => c.String(maxLength: 200));
            AddColumn("dbo.InsurableItem", "Description", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
