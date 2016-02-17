namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Operator", "InsurableItemID", "dbo.InsurableItem");
            DropIndex("dbo.Operator", new[] { "InsurableItemID" });
            DropColumn("dbo.Operator", "InsurableItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operator", "InsurableItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.Operator", "InsurableItemID");
            AddForeignKey("dbo.Operator", "InsurableItemID", "dbo.InsurableItem", "ID");
        }
    }
}
