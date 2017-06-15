namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipCategoryToJob : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.JobMasters", "CategoryId", c => c.Int(nullable: true));
            //CreateIndex("dbo.JobMasters", "CategoryId");
            //AddForeignKey("dbo.JobMasters", "CategoryId", "dbo.CategoryMasters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.JobMasters", "CategoryId", "dbo.CategoryMasters");
            //DropIndex("dbo.JobMasters", new[] { "CategoryId" });
            //DropColumn("dbo.JobMasters", "CategoryId");
        }
    }
}
