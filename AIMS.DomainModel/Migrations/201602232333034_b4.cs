namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegionContextParameterValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegionID = c.Int(nullable: false),
                        ContextParameterID = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContextParameter", t => t.ContextParameterID)
                .ForeignKey("dbo.Region", t => t.RegionID)
                .Index(t => t.RegionID)
                .Index(t => t.ContextParameterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegionContextParameterValue", "RegionID", "dbo.Region");
            DropForeignKey("dbo.RegionContextParameterValue", "ContextParameterID", "dbo.ContextParameter");
            DropIndex("dbo.RegionContextParameterValue", new[] { "ContextParameterID" });
            DropIndex("dbo.RegionContextParameterValue", new[] { "RegionID" });
            DropTable("dbo.RegionContextParameterValue");
        }
    }
}
