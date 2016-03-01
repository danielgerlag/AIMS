namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropIndex("dbo.PolicyTypeTransitionJournalTemplateInput", new[] { "AttributeLookupListID" });
            AddColumn("dbo.PolicyTypeTransitionInput", "AttributeLookupListID", c => c.Int());
            CreateIndex("dbo.PolicyTypeTransitionInput", "AttributeLookupListID");
            AddForeignKey("dbo.PolicyTypeTransitionInput", "AttributeLookupListID", "dbo.AttributeLookupList", "ID");
            DropColumn("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID", c => c.Int());
            DropForeignKey("dbo.PolicyTypeTransitionInput", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropIndex("dbo.PolicyTypeTransitionInput", new[] { "AttributeLookupListID" });
            DropColumn("dbo.PolicyTypeTransitionInput", "AttributeLookupListID");
            CreateIndex("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID");
            AddForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID", "dbo.AttributeLookupList", "ID");
        }
    }
}
