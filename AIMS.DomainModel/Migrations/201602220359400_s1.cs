namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransactionTriggerLog", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionTriggerLog", "EndTime", c => c.DateTime(nullable: false));
        }
    }
}
