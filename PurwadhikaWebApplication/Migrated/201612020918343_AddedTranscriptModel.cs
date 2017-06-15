namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTranscriptModel : DbMigration
    {
        public override void Up()
        {
        //    CreateTable(
        //        "dbo.TranscriptsModels",
        //        c => new
        //            {
        //                id = c.Int(nullable: false, identity: true),
        //                StudentId = c.String(maxLength: 128),
        //                FileName = c.String(),
        //                FileUrl = c.String(),
        //            })
        //        .PrimaryKey(t => t.id)
        //        .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
        //        .Index(t => t.StudentId);
            
        //    DropColumn("dbo.AspNetUsers", "AccountTranscript");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.AspNetUsers", "AccountTranscript", c => c.String());
            //DropForeignKey("dbo.TranscriptsModels", "StudentId", "dbo.AspNetUsers");
            //DropIndex("dbo.TranscriptsModels", new[] { "StudentId" });
            //DropTable("dbo.TranscriptsModels");
        }
    }
}
