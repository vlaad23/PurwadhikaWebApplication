namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editJobMaster_RenameProperty_and_AddJobStatus : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.JobMasters", "AvailablePosition", c => c.Int(nullable: false));
            //AddColumn("dbo.JobMasters", "ApprovalStatus", c => c.Int(nullable: true));
            //AddColumn("dbo.JobMasters", "JobStatus", c => c.Int(nullable: false));
            //DropColumn("dbo.JobMasters", "Quota");
            //DropColumn("dbo.JobMasters", "Status");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.JobMasters", "Status", c => c.String());
            //AddColumn("dbo.JobMasters", "Quota", c => c.Int(nullable: false));
            //DropColumn("dbo.JobMasters", "JobStatus");
            //DropColumn("dbo.JobMasters", "ApprovalStatus");
            //DropColumn("dbo.JobMasters", "AvailablePosition");
        }
    }
}
