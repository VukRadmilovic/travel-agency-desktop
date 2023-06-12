namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pls : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accommodations", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Places", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Restaurants", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Accommodations", new[] { "Trip_Id" });
            DropIndex("dbo.Places", new[] { "Trip_Id" });
            DropIndex("dbo.Restaurants", new[] { "Trip_Id" });
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
                        PlaceId = c.Int(nullable: false),
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
            
            DropColumn("dbo.Accommodations", "Trip_Id");
            DropColumn("dbo.Places", "Trip_Id");
            DropColumn("dbo.Restaurants", "Trip_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "Trip_Id", c => c.Int());
            AddColumn("dbo.Places", "Trip_Id", c => c.Int());
            AddColumn("dbo.Accommodations", "Trip_Id", c => c.Int());
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
            CreateIndex("dbo.Restaurants", "Trip_Id");
            CreateIndex("dbo.Places", "Trip_Id");
            CreateIndex("dbo.Accommodations", "Trip_Id");
            AddForeignKey("dbo.Restaurants", "Trip_Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.Places", "Trip_Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.Accommodations", "Trip_Id", "dbo.Trips", "Id");
        }
    }
}
