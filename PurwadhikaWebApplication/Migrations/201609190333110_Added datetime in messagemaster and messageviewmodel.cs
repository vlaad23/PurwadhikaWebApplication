namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeddatetimeinmessagemasterandmessageviewmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageMasters", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageMasters", "DateTime");
        }
    }
}
