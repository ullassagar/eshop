namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        ParentCategory_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Category", t => t.ParentCategory_CategoryId)
                .Index(t => t.ParentCategory_CategoryId);
            
            AddColumn("dbo.Product", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Product", "Category_CategoryId");
            AddForeignKey("dbo.Product", "Category_CategoryId", "dbo.Category", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Category_CategoryId", "dbo.Category");
            DropForeignKey("dbo.Category", "ParentCategory_CategoryId", "dbo.Category");
            DropIndex("dbo.Category", new[] { "ParentCategory_CategoryId" });
            DropIndex("dbo.Product", new[] { "Category_CategoryId" });
            DropColumn("dbo.Product", "Category_CategoryId");
            DropTable("dbo.Category");
        }
    }
}
