namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class puan_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Puans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Durum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Puans");
        }
    }
}
