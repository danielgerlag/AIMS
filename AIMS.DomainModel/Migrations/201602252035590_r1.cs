namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class r1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PolicySubType", "RatingProfileID", "dbo.RatingProfile");
            DropIndex("dbo.PolicySubType", new[] { "RatingProfileID" });
            CreateTable(
                "dbo.PolicySubTypeRatingProfile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PolicySubTypeID = c.Int(nullable: false),
                        RatingProfileID = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PolicySubType", t => t.PolicySubTypeID)
                .ForeignKey("dbo.RatingProfile", t => t.RatingProfileID)
                .Index(t => t.PolicySubTypeID)
                .Index(t => t.RatingProfileID);
            
            AddColumn("dbo.Policy", "RatesDate", c => c.DateTime());
            DropColumn("dbo.PolicySubType", "RatingProfileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PolicySubType", "RatingProfileID", c => c.Int());
            DropForeignKey("dbo.PolicySubTypeRatingProfile", "RatingProfileID", "dbo.RatingProfile");
            DropForeignKey("dbo.PolicySubTypeRatingProfile", "PolicySubTypeID", "dbo.PolicySubType");
            DropIndex("dbo.PolicySubTypeRatingProfile", new[] { "RatingProfileID" });
            DropIndex("dbo.PolicySubTypeRatingProfile", new[] { "PolicySubTypeID" });
            DropColumn("dbo.Policy", "RatesDate");
            DropTable("dbo.PolicySubTypeRatingProfile");
            CreateIndex("dbo.PolicySubType", "RatingProfileID");
            AddForeignKey("dbo.PolicySubType", "RatingProfileID", "dbo.RatingProfile", "ID");
        }
    }
}
