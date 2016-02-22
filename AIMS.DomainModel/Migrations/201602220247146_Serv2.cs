namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Serv2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionTriggerLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionTriggerID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Thread = c.Int(nullable: false),
                        MachineName = c.String(),
                        IsSuccess = c.Boolean(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        IsRequeue = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TransactionTrigger", t => t.TransactionTriggerID)
                .Index(t => t.TransactionTriggerID);
            
            CreateTable(
                "dbo.TransactionTriggerException",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionTriggerLogID = c.Int(nullable: false),
                        Message = c.String(),
                        ExceptionType = c.String(nullable: false, maxLength: 1),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TransactionTriggerLog", t => t.TransactionTriggerLogID)
                .Index(t => t.TransactionTriggerLogID);
            
            AddColumn("dbo.JournalTemplate", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.TransactionTrigger", "OnHold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTriggerLog", "TransactionTriggerID", "dbo.TransactionTrigger");
            DropForeignKey("dbo.TransactionTriggerException", "TransactionTriggerLogID", "dbo.TransactionTriggerLog");
            DropIndex("dbo.TransactionTriggerException", new[] { "TransactionTriggerLogID" });
            DropIndex("dbo.TransactionTriggerLog", new[] { "TransactionTriggerID" });
            DropColumn("dbo.TransactionTrigger", "OnHold");
            DropColumn("dbo.JournalTemplate", "Priority");
            DropTable("dbo.TransactionTriggerException");
            DropTable("dbo.TransactionTriggerLog");
        }
    }
}
