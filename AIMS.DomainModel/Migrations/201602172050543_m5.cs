namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operator", "OperatorTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Operator", "OperatorTypeID");
            AddForeignKey("dbo.Operator", "OperatorTypeID", "dbo.OperatorType", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operator", "OperatorTypeID", "dbo.OperatorType");
            DropIndex("dbo.Operator", new[] { "OperatorTypeID" });
            DropColumn("dbo.Operator", "OperatorTypeID");
        }
    }
}
