namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployerViewModel : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.EmployerViewModels",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Role = c.String(nullable: false),
            //        Email = c.String(nullable: false),
            //        Password = c.String(nullable: false, maxLength: 100),
            //        ConfirmPassword = c.String(),
            //        Address = c.String(nullable: false),
            //        AccountPicture = c.String(),
            //        InstanceName = c.String(nullable: false),
            //        Industry = c.String(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);


        }
        
        public override void Down()
        {
            //DropTable("dbo.EmployerViewModels");
        }
    }
}