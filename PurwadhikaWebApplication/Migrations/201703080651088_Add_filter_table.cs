namespace PurwadhikaWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_filter_table : DbMigration
    {
        public override void Up()
        {
            {
                CreateTable(
                    "dbo.FilterMaster",
                    c => new
                        {
                            Id = c.Int(nullable: false, identity: true),
                            FilterName = c.String(),
                        })
                    .PrimaryKey(t => t.Id);

            }
        }
        
        public override void Down()
        {
            DropTable("dbo.FilterMaster");
        }
    }
}
