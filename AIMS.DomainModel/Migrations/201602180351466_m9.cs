namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InsurableItem", "PolicyID", c => c.Int(nullable: false));
            CreateIndex("dbo.InsurableItem", "PolicyID");
            AddForeignKey("dbo.InsurableItem", "PolicyID", "dbo.Policy", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InsurableItem", "PolicyID", "dbo.Policy");
            DropIndex("dbo.InsurableItem", new[] { "PolicyID" });
            DropColumn("dbo.InsurableItem", "PolicyID");
        }
    }
}
