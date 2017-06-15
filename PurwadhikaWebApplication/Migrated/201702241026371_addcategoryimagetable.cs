namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategoryimagetable : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.CategoryMasters", "CategoryImage", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.CategoryMasters", "CategoryImage");
        }
    }
}
