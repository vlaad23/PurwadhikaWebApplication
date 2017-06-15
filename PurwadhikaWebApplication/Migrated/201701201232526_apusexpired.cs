namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apusexpired : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.JobMasters", "Expired");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.JobMasters", "Expired", c => c.Boolean(nullable: false));
        }
    }
}
