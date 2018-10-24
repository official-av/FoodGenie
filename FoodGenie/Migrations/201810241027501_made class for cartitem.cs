namespace FoodGenie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeclassforcartitem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        RecName_Id = c.Int(),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecName_Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.RecName_Id)
                .Index(t => t.Cart_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.CartItems", "RecName_Id", "dbo.Recipes");
            DropIndex("dbo.CartItems", new[] { "Cart_Id" });
            DropIndex("dbo.CartItems", new[] { "RecName_Id" });
            DropTable("dbo.CartItems");
        }
    }
}
