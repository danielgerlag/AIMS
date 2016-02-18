namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttributeDataType", "Code", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AttributeDataType", "Code");
        }
    }
}
