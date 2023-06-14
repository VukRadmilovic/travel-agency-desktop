namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neka_migracija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accommodations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        InfoLink = c.String(),
                        StarCount = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        InfoLink = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        InfoLink = c.String(),
                        IsVeganFriendly = c.Boolean(nullable: false),
                        IsGlutenFreeFriendly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TripBoughtOrReservedByUsers",
                c => new
                    {
                        TripId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        BuyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.TripId, t.UserId, t.Action });
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TripAccommodation",
                c => new
                    {
                        TripId = c.Int(nullable: false),
                        AccommodationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TripId, t.AccommodationId })
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .ForeignKey("dbo.Accommodations", t => t.AccommodationId, cascadeDelete: true)
                .Index(t => t.TripId)
                .Index(t => t.AccommodationId);
            
            CreateTable(
                "dbo.TripPlace",
                c => new
                    {
                        TripId = c.Int(nullable: false),
                        PlaceId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TripId, t.PlaceId })
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.TripId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.TripRestaurant",
                c => new
                    {
                        TripId = c.Int(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TripId, t.RestaurantId })
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.TripId)
                .Index(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TripRestaurant", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.TripRestaurant", "TripId", "dbo.Trips");
            DropForeignKey("dbo.TripPlace", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.TripPlace", "TripId", "dbo.Trips");
            DropForeignKey("dbo.TripAccommodation", "AccommodationId", "dbo.Accommodations");
            DropForeignKey("dbo.TripAccommodation", "TripId", "dbo.Trips");
            DropIndex("dbo.TripRestaurant", new[] { "RestaurantId" });
            DropIndex("dbo.TripRestaurant", new[] { "TripId" });
            DropIndex("dbo.TripPlace", new[] { "PlaceId" });
            DropIndex("dbo.TripPlace", new[] { "TripId" });
            DropIndex("dbo.TripAccommodation", new[] { "AccommodationId" });
            DropIndex("dbo.TripAccommodation", new[] { "TripId" });
            DropTable("dbo.TripRestaurant");
            DropTable("dbo.TripPlace");
            DropTable("dbo.TripAccommodation");
            DropTable("dbo.Users");
            DropTable("dbo.TripBoughtOrReservedByUsers");
            DropTable("dbo.Trips");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Places");
            DropTable("dbo.Accommodations");
        }
    }
}
