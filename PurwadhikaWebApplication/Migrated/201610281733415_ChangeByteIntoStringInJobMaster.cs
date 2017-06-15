namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeByteIntoStringInJobMaster : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobMasters", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobMasters", "Image", c => c.Binary());
        }
    }
}
