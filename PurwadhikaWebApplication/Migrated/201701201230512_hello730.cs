namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hello730 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.JobMasters", "Expired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.JobMasters", "Expired");
        }
    }
}
