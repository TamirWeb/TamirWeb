namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Click_added_Tamirci : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tamircilers", "Click", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tamircilers", "Click");
        }
    }
}
