namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kategori_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        kategori = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tamircilers", "Kategori_Id", c => c.Int());
            CreateIndex("dbo.Tamircilers", "Kategori_Id");
            AddForeignKey("dbo.Tamircilers", "Kategori_Id", "dbo.Kategoris", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tamircilers", "Kategori_Id", "dbo.Kategoris");
            DropIndex("dbo.Tamircilers", new[] { "Kategori_Id" });
            DropColumn("dbo.Tamircilers", "Kategori_Id");
            DropTable("dbo.Kategoris");
        }
    }
}
