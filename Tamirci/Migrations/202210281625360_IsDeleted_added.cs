namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeleted_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Hakkımızda", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mesajlars", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tamircilers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Yorumlars", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Yorumlars", "IsDeleted");
            DropColumn("dbo.Tamircilers", "IsDeleted");
            DropColumn("dbo.Mesajlars", "IsDeleted");
            DropColumn("dbo.Hakkımızda", "IsDeleted");
            DropColumn("dbo.Admins", "IsDeleted");
        }
    }
}
