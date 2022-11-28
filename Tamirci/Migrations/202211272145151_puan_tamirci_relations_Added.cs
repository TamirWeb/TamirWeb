namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class puan_tamirci_relations_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Puans", "Tamirciid", c => c.Int(nullable: false));
            CreateIndex("dbo.Puans", "Tamirciid");
            AddForeignKey("dbo.Puans", "Tamirciid", "dbo.Tamircilers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Puans", "Tamirciid", "dbo.Tamircilers");
            DropIndex("dbo.Puans", new[] { "Tamirciid" });
            DropColumn("dbo.Puans", "Tamirciid");
        }
    }
}
