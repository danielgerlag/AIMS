namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolicyTypeItemClass",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeID = c.Int(nullable: false),
                        InsurableItemClassID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InsurableItemClass", t => t.InsurableItemClassID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.InsurableItemClassID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyTypeItemClass", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeItemClass", "InsurableItemClassID", "dbo.InsurableItemClass");
            DropIndex("dbo.PolicyTypeItemClass", new[] { "InsurableItemClassID" });
            DropIndex("dbo.PolicyTypeItemClass", new[] { "PolicyTypeID" });
            DropTable("dbo.PolicyTypeItemClass");
        }
    }
}
