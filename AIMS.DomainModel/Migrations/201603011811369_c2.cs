namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID", c => c.Int());
            CreateIndex("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID");
            AddForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID", "dbo.AttributeLookupList", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID", "dbo.AttributeLookupList");
            DropIndex("dbo.PolicyTypeTransitionJournalTemplateInput", new[] { "AttributeLookupListID" });
            DropColumn("dbo.PolicyTypeTransitionJournalTemplateInput", "AttributeLookupListID");
        }
    }
}
