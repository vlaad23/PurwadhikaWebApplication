namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erroraddmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        Image = c.Binary(),
                        Quota = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobMasters");
        }
    }
}
