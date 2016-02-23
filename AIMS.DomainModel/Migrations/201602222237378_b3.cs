namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JournalTxn", "JournalTemplateTxnID", c => c.Int());
            AddColumn("dbo.JournalTxn", "PolicyCoverageID", c => c.Int());
            CreateIndex("dbo.JournalTxn", "JournalTemplateTxnID");
            CreateIndex("dbo.JournalTxn", "PolicyCoverageID");
            AddForeignKey("dbo.JournalTxn", "JournalTemplateTxnID", "dbo.JournalTemplateTxn", "ID");
            AddForeignKey("dbo.JournalTxn", "PolicyCoverageID", "dbo.PolicyCoverage", "ID");
            DropColumn("dbo.LedgerTxn", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LedgerTxn", "Description", c => c.String(nullable: false, maxLength: 300));
            DropForeignKey("dbo.JournalTxn", "PolicyCoverageID", "dbo.PolicyCoverage");
            DropForeignKey("dbo.JournalTxn", "JournalTemplateTxnID", "dbo.JournalTemplateTxn");
            DropIndex("dbo.JournalTxn", new[] { "PolicyCoverageID" });
            DropIndex("dbo.JournalTxn", new[] { "JournalTemplateTxnID" });
            DropColumn("dbo.JournalTxn", "PolicyCoverageID");
            DropColumn("dbo.JournalTxn", "JournalTemplateTxnID");
        }
    }
}
