namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agent4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegionTax",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegionID = c.Int(nullable: false),
                        ContextParameterID = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContextParameter", t => t.ContextParameterID)
                .ForeignKey("dbo.Region", t => t.RegionID)
                .Index(t => t.RegionID)
                .Index(t => t.ContextParameterID);
            
            CreateTable(
                "dbo.AgentTransactionTrigger",
                c => new
                    {
                        TransactionTriggerID = c.Int(nullable: false, identity: true),
                        AgentID = c.Int(nullable: false),
                        TransactionTrigger_ID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionTriggerID)
                .ForeignKey("dbo.TransactionTrigger", t => t.TransactionTrigger_ID)
                .Index(t => t.AgentID)
                .Index(t => t.TransactionTrigger_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgentTransactionTrigger", "TransactionTrigger_ID", "dbo.TransactionTrigger");
            DropForeignKey("dbo.RegionTax", "RegionID", "dbo.Region");
            DropForeignKey("dbo.RegionTax", "ContextParameterID", "dbo.ContextParameter");
            DropIndex("dbo.AgentTransactionTrigger", new[] { "TransactionTrigger_ID" });
            DropIndex("dbo.AgentTransactionTrigger", new[] { "AgentID" });
            DropIndex("dbo.RegionTax", new[] { "ContextParameterID" });
            DropIndex("dbo.RegionTax", new[] { "RegionID" });
            DropTable("dbo.AgentTransactionTrigger");
            DropTable("dbo.RegionTax");
        }
    }
}
