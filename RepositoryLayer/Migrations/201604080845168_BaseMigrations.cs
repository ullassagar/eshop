namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberName = c.String(),
                        EmailAddress = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductCount = c.Int(nullable: false),
                        PriceOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPriceOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductName = c.String(),
                        ImageUrl = c.String(),
                        Length = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderOrderStatus",
                c => new
                    {
                        OrderOrderStatusId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderOrderStatusId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        OrderValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentOrderStatus = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        OrderStatusName = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        EanCode = c.String(),
                        ImageUrl = c.String(),
                        Length = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsOutOfStock = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        EmailId = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropTable("dbo.User");
            DropTable("dbo.Product");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Order");
            DropTable("dbo.OrderOrderStatus");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Member");
        }
    }
}
