namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PolicyCoverage", "Limit", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PolicyCoverage", "Deductible", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PolicyCoverage", "Deductible");
            DropColumn("dbo.PolicyCoverage", "Limit");
        }
    }
}
