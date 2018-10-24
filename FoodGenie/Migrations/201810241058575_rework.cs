namespace FoodGenie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rework : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.AspNetUsers", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.AspNetUsers", new[] { "Cart_Id" });
            DropIndex("dbo.CartItems", new[] { "Cart_Id" });
            RenameColumn(table: "dbo.CartItems", name: "RecName_Id", newName: "Recipe_Id");
            RenameIndex(table: "dbo.CartItems", name: "IX_RecName_Id", newName: "IX_Recipe_Id");
            AddColumn("dbo.CartItems", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CartItems", "ApplicationUser_Id");
            AddForeignKey("dbo.CartItems", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "Cart_Id");
            DropColumn("dbo.CartItems", "Cart_Id");
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CartItems", "Cart_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Cart_Id", c => c.Int());
            DropForeignKey("dbo.CartItems", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CartItems", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.CartItems", "ApplicationUser_Id");
            RenameIndex(table: "dbo.CartItems", name: "IX_Recipe_Id", newName: "IX_RecName_Id");
            RenameColumn(table: "dbo.CartItems", name: "Recipe_Id", newName: "RecName_Id");
            CreateIndex("dbo.CartItems", "Cart_Id");
            CreateIndex("dbo.AspNetUsers", "Cart_Id");
            AddForeignKey("dbo.AspNetUsers", "Cart_Id", "dbo.Carts", "Id");
            AddForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts", "Id");
        }
    }
}
