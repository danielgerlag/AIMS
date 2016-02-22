namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.JournalTemplateTxnPosting", new[] { "LedgerAccountID" });
            AddColumn("dbo.Journal", "Reference", c => c.String(maxLength: 100));
            AddColumn("dbo.JournalTemplate", "SequenceNumberID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "AmountContextParameterID", c => c.Int());
            AddColumn("dbo.JournalTemplateTxn", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.JournalTemplateTxnPosting", "CoverageExpenseAccount", c => c.Boolean(nullable: false));
            AddColumn("dbo.JournalTemplateTxnPosting", "CoverageIncomeAccount", c => c.Boolean(nullable: false));
            AlterColumn("dbo.JournalTemplateTxnPosting", "LedgerAccountID", c => c.Int());
            CreateIndex("dbo.JournalTemplate", "SequenceNumberID");
            CreateIndex("dbo.JournalTemplateTxn", "AmountContextParameterID");
            CreateIndex("dbo.JournalTemplateTxnPosting", "LedgerAccountID");
            AddForeignKey("dbo.JournalTemplateTxn", "AmountContextParameterID", "dbo.ContextParameter", "ID");
            AddForeignKey("dbo.JournalTemplate", "SequenceNumberID", "dbo.SequenceNumber", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JournalTemplate", "SequenceNumberID", "dbo.SequenceNumber");
            DropForeignKey("dbo.JournalTemplateTxn", "AmountContextParameterID", "dbo.ContextParameter");
            DropIndex("dbo.JournalTemplateTxnPosting", new[] { "LedgerAccountID" });
            DropIndex("dbo.JournalTemplateTxn", new[] { "AmountContextParameterID" });
            DropIndex("dbo.JournalTemplate", new[] { "SequenceNumberID" });
            AlterColumn("dbo.JournalTemplateTxnPosting", "LedgerAccountID", c => c.Int(nullable: false));
            DropColumn("dbo.JournalTemplateTxnPosting", "CoverageIncomeAccount");
            DropColumn("dbo.JournalTemplateTxnPosting", "CoverageExpenseAccount");
            DropColumn("dbo.JournalTemplateTxn", "Amount");
            DropColumn("dbo.JournalTemplateTxn", "AmountContextParameterID");
            DropColumn("dbo.JournalTemplate", "SequenceNumberID");
            DropColumn("dbo.Journal", "Reference");
            CreateIndex("dbo.JournalTemplateTxnPosting", "LedgerAccountID");
        }
    }
}
