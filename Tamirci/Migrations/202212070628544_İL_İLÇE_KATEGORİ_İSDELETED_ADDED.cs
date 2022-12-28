namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class İL_İLÇE_KATEGORİ_İSDELETED_ADDED : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.İlçe", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.İl", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Kategoris", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kategoris", "IsDeleted");
            DropColumn("dbo.İl", "IsDeleted");
            DropColumn("dbo.İlçe", "IsDeleted");
        }
    }
}
