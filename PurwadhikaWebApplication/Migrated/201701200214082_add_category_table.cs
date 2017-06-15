namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_category_table : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CategoryMasters",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CategoryName = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.CategoryMasters");
        }
    }
}
