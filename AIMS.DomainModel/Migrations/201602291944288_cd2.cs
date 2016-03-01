namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactDetailType", "IsTwitter", c => c.Boolean(nullable: false));
            AddColumn("dbo.ContactDetailType", "IsFacebook", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactDetailType", "IsFacebook");
            DropColumn("dbo.ContactDetailType", "IsTwitter");
        }
    }
}
