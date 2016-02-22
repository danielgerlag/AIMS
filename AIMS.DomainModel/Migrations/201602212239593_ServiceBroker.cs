namespace AIMS.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceBroker : DbMigration
    {
        public override void Up()
        {
            Sql(GetSqlResource("Queues"));
            Sql(GetSqlResource("EnqueueMessage"));
        }
        
        public override void Down()
        {
        }

        private string GetSqlResource(string name)
        {
            return (Resources.GetObject(name) as string);
            //System.IO.StreamReader rdr = new System.IO.StreamReader(Resources.GetStream(name));
            //return rdr.ReadToEnd();            
        }
    }
}
