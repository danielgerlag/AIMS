namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Policy", new[] { "BillingPublicID" });
            AlterColumn("dbo.Policy", "BillingPublicID", c => c.Int());
            CreateIndex("dbo.Policy", "BillingPublicID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Policy", new[] { "BillingPublicID" });
            AlterColumn("dbo.Policy", "BillingPublicID", c => c.Int(nullable: false));
            CreateIndex("dbo.Policy", "BillingPublicID");
        }
    }
}
