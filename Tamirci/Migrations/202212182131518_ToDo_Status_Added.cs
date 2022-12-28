namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDo_Status_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Status");
        }
    }
}
