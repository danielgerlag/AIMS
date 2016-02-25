namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.JournalTemplateInput", new[] { "JournalTemplateID" });
            DropIndex("dbo.JournalTemplateInput", new[] { "JournalTemplate_ID" });
            DropColumn("dbo.JournalTemplateInput", "JournalTemplateID");
            RenameColumn(table: "dbo.JournalTemplateInput", name: "JournalTemplate_ID", newName: "JournalTemplateID");
            AlterColumn("dbo.JournalTemplateInput", "JournalTemplateID", c => c.Int(nullable: false));
            CreateIndex("dbo.JournalTemplateInput", "JournalTemplateID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.JournalTemplateInput", new[] { "JournalTemplateID" });
            AlterColumn("dbo.JournalTemplateInput", "JournalTemplateID", c => c.Int());
            RenameColumn(table: "dbo.JournalTemplateInput", name: "JournalTemplateID", newName: "JournalTemplate_ID");
            AddColumn("dbo.JournalTemplateInput", "JournalTemplateID", c => c.Int(nullable: false));
            CreateIndex("dbo.JournalTemplateInput", "JournalTemplate_ID");
            CreateIndex("dbo.JournalTemplateInput", "JournalTemplateID");
        }
    }
}
