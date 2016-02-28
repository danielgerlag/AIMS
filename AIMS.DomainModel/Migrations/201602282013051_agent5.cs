namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agent5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AgentTransactionTrigger", new[] { "TransactionTrigger_ID" });
            DropColumn("dbo.AgentTransactionTrigger", "TransactionTriggerID");
            RenameColumn(table: "dbo.AgentTransactionTrigger", name: "TransactionTrigger_ID", newName: "TransactionTriggerID");
            DropPrimaryKey("dbo.AgentTransactionTrigger");
            AlterColumn("dbo.AgentTransactionTrigger", "TransactionTriggerID", c => c.Int(nullable: false));
            AlterColumn("dbo.AgentTransactionTrigger", "TransactionTriggerID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AgentTransactionTrigger", "TransactionTriggerID");
            CreateIndex("dbo.AgentTransactionTrigger", "TransactionTriggerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AgentTransactionTrigger", new[] { "TransactionTriggerID" });
            DropPrimaryKey("dbo.AgentTransactionTrigger");
            AlterColumn("dbo.AgentTransactionTrigger", "TransactionTriggerID", c => c.Int());
            AlterColumn("dbo.AgentTransactionTrigger", "TransactionTriggerID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.AgentTransactionTrigger", "TransactionTriggerID");
            RenameColumn(table: "dbo.AgentTransactionTrigger", name: "TransactionTriggerID", newName: "TransactionTrigger_ID");
            AddColumn("dbo.AgentTransactionTrigger", "TransactionTriggerID", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.AgentTransactionTrigger", "TransactionTrigger_ID");
        }
    }
}
