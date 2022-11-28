namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class control_information_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tamircilers", "control", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tamircilers", "control");
        }
    }
}
