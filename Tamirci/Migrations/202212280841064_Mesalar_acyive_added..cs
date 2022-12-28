namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mesalar_acyive_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mesajlars", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesajlars", "Active");
        }
    }
}
