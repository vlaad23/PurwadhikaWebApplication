namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfirstnamelastnameinidentitymodelsanduserviewmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Experience", c => c.String());
            DropColumn("dbo.AspNetUsers", "Fullname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Fullname", c => c.String());
            DropColumn("dbo.AspNetUsers", "Experience");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
        }
    }
}
