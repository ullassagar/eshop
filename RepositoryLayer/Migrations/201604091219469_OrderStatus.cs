namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "LatestOrderStatusId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "LatestOrderStatusId");
        }
    }
}
