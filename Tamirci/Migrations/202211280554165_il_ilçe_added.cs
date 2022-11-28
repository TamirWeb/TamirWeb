namespace Tamirci.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class il_ilçe_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.İlçe",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        İlid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.İl", t => t.İlid, cascadeDelete: true)
                .Index(t => t.İlid);
            
            CreateTable(
                "dbo.İl",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.İlçe", "İlid", "dbo.İl");
            DropIndex("dbo.İlçe", new[] { "İlid" });
            DropTable("dbo.İl");
            DropTable("dbo.İlçe");
        }
    }
}
