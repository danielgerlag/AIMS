namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journal", "Amount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journal", "Amount");
        }
    }
}
