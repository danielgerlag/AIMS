namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuses2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PolicyTypeStatus", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicyTypeStatus", new[] { "PolicyType_ID" });
            DropColumn("dbo.PolicyTypeStatus", "PolicyTypeID");
            RenameColumn(table: "dbo.PolicyTypeStatus", name: "PolicyType_ID", newName: "PolicyTypeID");
            AlterColumn("dbo.PolicyTypeStatus", "PolicyTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.PolicyTypeStatus", "PolicyTypeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PolicyTypeStatus", new[] { "PolicyTypeID" });
            AlterColumn("dbo.PolicyTypeStatus", "PolicyTypeID", c => c.Int());
            RenameColumn(table: "dbo.PolicyTypeStatus", name: "PolicyTypeID", newName: "PolicyType_ID");
            AddColumn("dbo.PolicyTypeStatus", "PolicyTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.PolicyTypeStatus", "PolicyType_ID");
            CreateIndex("dbo.PolicyTypeStatus", "PolicyTypeID");
        }
    }
}
