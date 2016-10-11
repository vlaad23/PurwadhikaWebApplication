namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedpropertiesinIdentityModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Fullname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "Batch", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccountStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccountTranscript", c => c.String());
            AddColumn("dbo.AspNetUsers", "AccountPicture", c => c.String());
            AddColumn("dbo.AspNetUsers", "InstanceName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Industry", c => c.String());
            AddColumn("dbo.AspNetUsers", "About", c => c.String());
            AddColumn("dbo.AspNetUsers", "skills", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "skills");
            DropColumn("dbo.AspNetUsers", "About");
            DropColumn("dbo.AspNetUsers", "Industry");
            DropColumn("dbo.AspNetUsers", "InstanceName");
            DropColumn("dbo.AspNetUsers", "AccountPicture");
            DropColumn("dbo.AspNetUsers", "AccountTranscript");
            DropColumn("dbo.AspNetUsers", "AccountStatus");
            DropColumn("dbo.AspNetUsers", "Batch");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Fullname");
        }
    }
}
