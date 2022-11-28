namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class control_information_added1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Yorumlars", "control", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mesajlars", "control", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mesajlars", "control");
            DropColumn("dbo.Yorumlars", "control");
        }
    }
}
