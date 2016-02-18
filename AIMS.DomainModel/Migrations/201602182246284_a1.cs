namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JournalTxn", "TxnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LedgerTxn", "TxnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.JournalTemplateTxn", "JournalTxnClassID", c => c.Int(nullable: false));
            AddColumn("dbo.JournalTemplateTxn", "AmountInputID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "ReferenceInputID", c => c.Int());
            AddColumn("dbo.TransactionTriggerInput", "Value", c => c.String(maxLength: 500));
            AddColumn("dbo.TransactionTrigger", "NextExecutionDate", c => c.DateTime());
            AddColumn("dbo.TransactionTrigger", "EffectiveFrom", c => c.DateTime());
            AddColumn("dbo.TransactionTrigger", "EffectiveTo", c => c.DateTime());
            CreateIndex("dbo.JournalTemplateTxn", "JournalTxnClassID");
            CreateIndex("dbo.JournalTemplateTxn", "AmountInputID");
            CreateIndex("dbo.JournalTemplateTxn", "ReferenceInputID");
            AddForeignKey("dbo.JournalTemplateTxn", "AmountInputID", "dbo.JournalTemplateInput", "ID");
            AddForeignKey("dbo.JournalTemplateTxn", "JournalTxnClassID", "dbo.JournalTxnClass", "ID");
            AddForeignKey("dbo.JournalTemplateTxn", "ReferenceInputID", "dbo.JournalTemplateInput", "ID");
            DropColumn("dbo.TransactionTrigger", "ExecutionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionTrigger", "ExecutionDate", c => c.DateTime());
            DropForeignKey("dbo.JournalTemplateTxn", "ReferenceInputID", "dbo.JournalTemplateInput");
            DropForeignKey("dbo.JournalTemplateTxn", "JournalTxnClassID", "dbo.JournalTxnClass");
            DropForeignKey("dbo.JournalTemplateTxn", "AmountInputID", "dbo.JournalTemplateInput");
            DropIndex("dbo.JournalTemplateTxn", new[] { "ReferenceInputID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "AmountInputID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "JournalTxnClassID" });
            DropColumn("dbo.TransactionTrigger", "EffectiveTo");
            DropColumn("dbo.TransactionTrigger", "EffectiveFrom");
            DropColumn("dbo.TransactionTrigger", "NextExecutionDate");
            DropColumn("dbo.TransactionTriggerInput", "Value");
            DropColumn("dbo.JournalTemplateTxn", "ReferenceInputID");
            DropColumn("dbo.JournalTemplateTxn", "AmountInputID");
            DropColumn("dbo.JournalTemplateTxn", "JournalTxnClassID");
            DropColumn("dbo.LedgerTxn", "TxnDate");
            DropColumn("dbo.JournalTxn", "TxnDate");
        }
    }
}
