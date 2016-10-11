namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingAnnouncementMaster : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnnouncementMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        From = c.String(),
                        To = c.String(),
                        Message = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnnouncementMasters");
        }
    }
}
