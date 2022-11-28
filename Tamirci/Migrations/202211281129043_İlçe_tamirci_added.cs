namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class İlçe_tamirci_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tamircilers", "Tamirci_İlçe", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tamircilers", "Tamirci_İlçe");
        }
    }
}
