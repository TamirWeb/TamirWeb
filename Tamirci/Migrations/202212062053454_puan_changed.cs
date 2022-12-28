namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class puan_changed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Puans", "Durum", c => c.Boolean(nullable: false));
            DropColumn("dbo.Puans", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Puans", "Rating", c => c.Int(nullable: false));
            DropColumn("dbo.Puans", "Durum");
        }
    }
}
