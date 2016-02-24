namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolicyContextParameterValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyID = c.Int(nullable: false),
                        ContextParameterID = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContextParameter", t => t.ContextParameterID)
                .ForeignKey("dbo.Policy", t => t.PolicyID)
                .Index(t => t.PolicyID)
                .Index(t => t.ContextParameterID);
            
            CreateTable(
                "dbo.PolicySubTypeContextParameterValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicySubTypeID = c.Int(nullable: false),
                        ContextParameterID = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContextParameter", t => t.ContextParameterID)
                .ForeignKey("dbo.PolicySubType", t => t.PolicySubTypeID)
                .Index(t => t.PolicySubTypeID)
                .Index(t => t.ContextParameterID);
            
            CreateTable(
                "dbo.PolicyTypeContextParameterValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicyTypeID = c.Int(nullable: false),
                        ContextParameterID = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContextParameter", t => t.ContextParameterID)
                .ForeignKey("dbo.PolicyType", t => t.PolicyTypeID)
                .Index(t => t.PolicyTypeID)
                .Index(t => t.ContextParameterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyTypeContextParameterValue", "PolicyTypeID", "dbo.PolicyType");
            DropForeignKey("dbo.PolicyTypeContextParameterValue", "ContextParameterID", "dbo.ContextParameter");
            DropForeignKey("dbo.PolicySubTypeContextParameterValue", "PolicySubTypeID", "dbo.PolicySubType");
            DropForeignKey("dbo.PolicySubTypeContextParameterValue", "ContextParameterID", "dbo.ContextParameter");
            DropForeignKey("dbo.PolicyContextParameterValue", "PolicyID", "dbo.Policy");
            DropForeignKey("dbo.PolicyContextParameterValue", "ContextParameterID", "dbo.ContextParameter");
            DropIndex("dbo.PolicyTypeContextParameterValue", new[] { "ContextParameterID" });
            DropIndex("dbo.PolicyTypeContextParameterValue", new[] { "PolicyTypeID" });
            DropIndex("dbo.PolicySubTypeContextParameterValue", new[] { "ContextParameterID" });
            DropIndex("dbo.PolicySubTypeContextParameterValue", new[] { "PolicySubTypeID" });
            DropIndex("dbo.PolicyContextParameterValue", new[] { "ContextParameterID" });
            DropIndex("dbo.PolicyContextParameterValue", new[] { "PolicyID" });
            DropTable("dbo.PolicyTypeContextParameterValue");
            DropTable("dbo.PolicySubTypeContextParameterValue");
            DropTable("dbo.PolicyContextParameterValue");
        }
    }
}
