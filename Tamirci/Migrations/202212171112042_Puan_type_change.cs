namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Puan_type_change : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tamircilers", "Tamirci_Puan", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tamircilers", "Tamirci_Puan", c => c.Single(nullable: false));
        }
    }
}
