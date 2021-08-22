namespace RentWebProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAndAddColumnOfImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administer",
                c => new
                    {
                        AdministerID = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AdministerID);
            
            CreateTable(
                "dbo.BranchStores",
                c => new
                    {
                        StoreID = c.Int(nullable: false, identity: true),
                        StoreName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        Fax = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.StoreID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DeliverID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                        OrderStatusID = c.String(maxLength: 20),
                        MemberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.DeliveryOptions", t => t.DeliverID)
                .ForeignKey("dbo.BranchStores", t => t.StoreID)
                .Index(t => t.DeliverID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.DeliveryOptions",
                c => new
                    {
                        DeliverID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.DeliverID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.String(nullable: false, maxLength: 8),
                        DailyRate = c.Decimal(storeType: "money"),
                        TotalAmount = c.Decimal(storeType: "money"),
                        StartDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        Returned = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 8),
                        ProductName = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        DailyRate = c.Decimal(storeType: "money"),
                        Available = c.Boolean(),
                        LaunchDate = c.DateTime(),
                        WithdrawalDate = c.DateTime(),
                        Discontinuation = c.Boolean(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        MemberID = c.Int(nullable: false),
                        ProductID = c.String(nullable: false, maxLength: 8),
                        StartDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.MemberID, t.ProductID })
                .ForeignKey("dbo.Members", t => t.MemberID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.MemberID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        Account = c.String(maxLength: 50),
                        PasswordHash = c.String(maxLength: 50),
                        FullName = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        Address = c.String(maxLength: 100),
                        Birthday = c.DateTime(storeType: "date"),
                        SignWayID = c.Int(nullable: false),
                        active = c.Boolean(),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.SignWay", t => t.SignWayID)
                .Index(t => t.SignWayID);
            
            CreateTable(
                "dbo.SignWay",
                c => new
                    {
                        SignWayID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SignWayID);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ImageID = c.Int(nullable: false),
                        ProductID = c.String(nullable: false, maxLength: 8),
                        Source = c.String(),
                    })
                .PrimaryKey(t => new { t.ImageID, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.String(nullable: false, maxLength: 3),
                        CategoryName = c.String(maxLength: 20),
                        ImageSrcMain = c.String(),
                        ImageSrcSecond = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        CategoryID = c.String(nullable: false, maxLength: 3),
                        SubCategoryID = c.String(nullable: false, maxLength: 2),
                        SubCategoryName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => new { t.CategoryID, t.SubCategoryID })
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategory", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Orders", "StoreID", "dbo.BranchStores");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.ProductImages", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Carts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Members", "SignWayID", "dbo.SignWay");
            DropForeignKey("dbo.Carts", "MemberID", "dbo.Members");
            DropForeignKey("dbo.Orders", "DeliverID", "dbo.DeliveryOptions");
            DropIndex("dbo.SubCategory", new[] { "CategoryID" });
            DropIndex("dbo.ProductImages", new[] { "ProductID" });
            DropIndex("dbo.Members", new[] { "SignWayID" });
            DropIndex("dbo.Carts", new[] { "ProductID" });
            DropIndex("dbo.Carts", new[] { "MemberID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "StoreID" });
            DropIndex("dbo.Orders", new[] { "DeliverID" });
            DropTable("dbo.SubCategory");
            DropTable("dbo.Categories");
            DropTable("dbo.ProductImages");
            DropTable("dbo.SignWay");
            DropTable("dbo.Members");
            DropTable("dbo.Carts");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.DeliveryOptions");
            DropTable("dbo.Orders");
            DropTable("dbo.BranchStores");
            DropTable("dbo.Administer");
        }
    }
}
