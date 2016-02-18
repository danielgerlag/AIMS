namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolicySubTypeCoverage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicySubTypeID = c.Int(nullable: false),
                        CoverageTypeID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CoverageType", t => t.CoverageTypeID)
                .ForeignKey("dbo.PolicySubType", t => t.PolicySubTypeID)
                .Index(t => t.PolicySubTypeID)
                .Index(t => t.CoverageTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicySubTypeCoverage", "PolicySubTypeID", "dbo.PolicySubType");
            DropForeignKey("dbo.PolicySubTypeCoverage", "CoverageTypeID", "dbo.CoverageType");
            DropIndex("dbo.PolicySubTypeCoverage", new[] { "CoverageTypeID" });
            DropIndex("dbo.PolicySubTypeCoverage", new[] { "PolicySubTypeID" });
            DropTable("dbo.PolicySubTypeCoverage");
        }
    }
}
