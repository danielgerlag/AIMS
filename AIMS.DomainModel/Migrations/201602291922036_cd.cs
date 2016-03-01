namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDetailType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        RegularExpression = c.String(maxLength: 300),
                        IsPhone = c.Boolean(nullable: false),
                        IsMobile = c.Boolean(nullable: false),
                        IsFixed = c.Boolean(nullable: false),
                        IsBusiness = c.Boolean(nullable: false),
                        IsAddress = c.Boolean(nullable: false),
                        IsFax = c.Boolean(nullable: false),
                        IsEmail = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DateCreatedUTC = c.DateTime(nullable: false),
                        DateModifiedUTC = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ContactDetail", "ContactDetailTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.ContactDetail", "ContactDetailTypeID");
            AddForeignKey("dbo.ContactDetail", "ContactDetailTypeID", "dbo.ContactDetailType", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactDetail", "ContactDetailTypeID", "dbo.ContactDetailType");
            DropIndex("dbo.ContactDetail", new[] { "ContactDetailTypeID" });
            DropColumn("dbo.ContactDetail", "ContactDetailTypeID");
            DropTable("dbo.ContactDetailType");
        }
    }
}
