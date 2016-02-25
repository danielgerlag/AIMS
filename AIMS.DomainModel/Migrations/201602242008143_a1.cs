namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PolicyTransactionTrigger", "TransactionTriggerID");
            DropColumn("dbo.ReportingEntityTransactionTrigger", "TransactionTriggerID");
            RenameColumn(table: "dbo.PolicyTransactionTrigger", name: "ID", newName: "TransactionTriggerID");
            RenameColumn(table: "dbo.ReportingEntityTransactionTrigger", name: "ID", newName: "TransactionTriggerID");
            RenameIndex(table: "dbo.PolicyTransactionTrigger", name: "IX_ID", newName: "IX_TransactionTriggerID");
            RenameIndex(table: "dbo.ReportingEntityTransactionTrigger", name: "IX_ID", newName: "IX_TransactionTriggerID");
            DropColumn("dbo.PolicyTransactionTrigger", "RowVersion");
            DropColumn("dbo.PolicyTransactionTrigger", "DateCreatedUTC");
            DropColumn("dbo.PolicyTransactionTrigger", "DateModifiedUTC");
            DropColumn("dbo.ReportingEntityTransactionTrigger", "RowVersion");
            DropColumn("dbo.ReportingEntityTransactionTrigger", "DateCreatedUTC");
            DropColumn("dbo.ReportingEntityTransactionTrigger", "DateModifiedUTC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReportingEntityTransactionTrigger", "DateModifiedUTC", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReportingEntityTransactionTrigger", "DateCreatedUTC", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReportingEntityTransactionTrigger", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.PolicyTransactionTrigger", "DateModifiedUTC", c => c.DateTime(nullable: false));
            AddColumn("dbo.PolicyTransactionTrigger", "DateCreatedUTC", c => c.DateTime(nullable: false));
            AddColumn("dbo.PolicyTransactionTrigger", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            RenameIndex(table: "dbo.ReportingEntityTransactionTrigger", name: "IX_TransactionTriggerID", newName: "IX_ID");
            RenameIndex(table: "dbo.PolicyTransactionTrigger", name: "IX_TransactionTriggerID", newName: "IX_ID");
            RenameColumn(table: "dbo.ReportingEntityTransactionTrigger", name: "TransactionTriggerID", newName: "ID");
            RenameColumn(table: "dbo.PolicyTransactionTrigger", name: "TransactionTriggerID", newName: "ID");
            AddColumn("dbo.ReportingEntityTransactionTrigger", "TransactionTriggerID", c => c.Int(nullable: false));
            AddColumn("dbo.PolicyTransactionTrigger", "TransactionTriggerID", c => c.Int(nullable: false));
        }
    }
}
