namespace RepositoryLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreationDateadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "CreationDate");
        }
    }
}
