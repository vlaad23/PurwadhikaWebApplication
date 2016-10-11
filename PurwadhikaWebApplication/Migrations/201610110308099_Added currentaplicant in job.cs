namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcurrentaplicantinjob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobMasters", "CurrentApplicant", c => c.Int(nullable: false));
            AddColumn("dbo.JobViewModels", "CurrentApplicant", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobViewModels", "CurrentApplicant");
            DropColumn("dbo.JobMasters", "CurrentApplicant");
        }
    }
}
