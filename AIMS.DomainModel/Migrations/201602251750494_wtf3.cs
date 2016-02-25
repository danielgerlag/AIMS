namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingProfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Engine = c.String(nullable: false, maxLength: 50),
                        ScriptLanguage = c.String(maxLength: 50),
                        Script = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PolicySubType", "RatingProfileID", c => c.Int());
            CreateIndex("dbo.PolicySubType", "RatingProfileID");
            AddForeignKey("dbo.PolicySubType", "RatingProfileID", "dbo.RatingProfile", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicySubType", "RatingProfileID", "dbo.RatingProfile");
            DropIndex("dbo.PolicySubType", new[] { "RatingProfileID" });
            DropColumn("dbo.PolicySubType", "RatingProfileID");
            DropTable("dbo.RatingProfile");
        }
    }
}
