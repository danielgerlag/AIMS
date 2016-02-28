namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dp1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContextParameterValue", "Value", c => c.Decimal(nullable: false, precision: 14, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContextParameterValue", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
