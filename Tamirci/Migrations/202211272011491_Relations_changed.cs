namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relations_changed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tamircilers", "Kategori_Id", "dbo.Kategoris");
            DropForeignKey("dbo.Yorumlars", "Tamirci_ID", "dbo.Tamircilers");
            DropIndex("dbo.Tamircilers", new[] { "Kategori_Id" });
            DropIndex("dbo.Yorumlars", new[] { "Tamirci_ID" });
            RenameColumn(table: "dbo.Tamircilers", name: "Kategori_Id", newName: "Kategoriid");
            RenameColumn(table: "dbo.Yorumlars", name: "Tamirci_ID", newName: "Tamirciid");
            AlterColumn("dbo.Tamircilers", "Kategoriid", c => c.Int(nullable: false));
            AlterColumn("dbo.Yorumlars", "Tamirciid", c => c.Int(nullable: false));
            CreateIndex("dbo.Tamircilers", "Kategoriid");
            CreateIndex("dbo.Yorumlars", "Tamirciid");
            AddForeignKey("dbo.Tamircilers", "Kategoriid", "dbo.Kategoris", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Yorumlars", "Tamirciid", "dbo.Tamircilers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorumlars", "Tamirciid", "dbo.Tamircilers");
            DropForeignKey("dbo.Tamircilers", "Kategoriid", "dbo.Kategoris");
            DropIndex("dbo.Yorumlars", new[] { "Tamirciid" });
            DropIndex("dbo.Tamircilers", new[] { "Kategoriid" });
            AlterColumn("dbo.Yorumlars", "Tamirciid", c => c.Int());
            AlterColumn("dbo.Tamircilers", "Kategoriid", c => c.Int());
            RenameColumn(table: "dbo.Yorumlars", name: "Tamirciid", newName: "Tamirci_ID");
            RenameColumn(table: "dbo.Tamircilers", name: "Kategoriid", newName: "Kategori_Id");
            CreateIndex("dbo.Yorumlars", "Tamirci_ID");
            CreateIndex("dbo.Tamircilers", "Kategori_Id");
            AddForeignKey("dbo.Yorumlars", "Tamirci_ID", "dbo.Tamircilers", "ID");
            AddForeignKey("dbo.Tamircilers", "Kategori_Id", "dbo.Kategoris", "Id");
        }
    }
}
