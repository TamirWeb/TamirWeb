namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salt_added_password : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "Salt");
        }
    }
}
