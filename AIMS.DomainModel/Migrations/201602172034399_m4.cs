namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.InsurableItemOperator", new[] { "InsurableItemID" });
            CreateIndex("dbo.InsurableItemOperator", "InsurableItemID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.InsurableItemOperator", new[] { "InsurableItemID" });
            CreateIndex("dbo.InsurableItemOperator", "InsurableItemID");
        }
    }
}
