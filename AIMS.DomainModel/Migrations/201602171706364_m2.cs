namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Operator", "OperatorTypeID", "dbo.OperatorType");
            DropIndex("dbo.Operator", new[] { "OperatorTypeID" });
            CreateTable(
                "dbo.InsurableItemOperator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsurableItemID = c.Int(nullable: false),
                        OperatorID = c.Int(nullable: false),
                        Primary = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Operator", t => t.OperatorID)
                .Index(t => t.InsurableItemID)
                .Index(t => t.OperatorID);
            
            AddColumn("dbo.InsurableItemClass", "DisplayName", c => c.String(maxLength: 200));
            AddColumn("dbo.Operator", "PolicyID", c => c.Int(nullable: false));
            CreateIndex("dbo.Operator", "PolicyID");
            AddForeignKey("dbo.Operator", "PolicyID", "dbo.Policy", "ID");
            DropColumn("dbo.Operator", "OperatorTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operator", "OperatorTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.InsurableItemOperator", "OperatorID", "dbo.Operator");
            DropForeignKey("dbo.Operator", "PolicyID", "dbo.Policy");
            DropIndex("dbo.Operator", new[] { "PolicyID" });
            DropIndex("dbo.InsurableItemOperator", new[] { "OperatorID" });
            DropIndex("dbo.InsurableItemOperator", new[] { "InsurableItemID" });
            DropColumn("dbo.Operator", "PolicyID");
            DropColumn("dbo.InsurableItemClass", "DisplayName");
            DropTable("dbo.InsurableItemOperator");
            CreateIndex("dbo.Operator", "OperatorTypeID");
            AddForeignKey("dbo.Operator", "OperatorTypeID", "dbo.OperatorType", "ID");
        }
    }
}
