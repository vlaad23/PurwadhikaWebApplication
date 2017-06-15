namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editapprovalstatustonullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobMasters", "ApprovalStatus", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
