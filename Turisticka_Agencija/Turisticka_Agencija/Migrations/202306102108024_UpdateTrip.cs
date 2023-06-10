namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTrip : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "Accommodation_Id", "dbo.Accommodations");
            DropForeignKey("dbo.Trips", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Trips", new[] { "Accommodation_Id" });
            DropIndex("dbo.Trips", new[] { "Restaurant_Id" });
            AddColumn("dbo.Accommodations", "Trip_Id", c => c.Int());
            AddColumn("dbo.Restaurants", "Trip_Id", c => c.Int());
            AddColumn("dbo.Trips", "QuantitySold", c => c.Int(nullable: false));
            CreateIndex("dbo.Accommodations", "Trip_Id");
            CreateIndex("dbo.Restaurants", "Trip_Id");
            AddForeignKey("dbo.Accommodations", "Trip_Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.Restaurants", "Trip_Id", "dbo.Trips", "Id");
            DropColumn("dbo.Trips", "Accommodation_Id");
            DropColumn("dbo.Trips", "Restaurant_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "Restaurant_Id", c => c.Int());
            AddColumn("dbo.Trips", "Accommodation_Id", c => c.Int());
            DropForeignKey("dbo.Restaurants", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Accommodations", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Restaurants", new[] { "Trip_Id" });
            DropIndex("dbo.Accommodations", new[] { "Trip_Id" });
            DropColumn("dbo.Trips", "QuantitySold");
            DropColumn("dbo.Restaurants", "Trip_Id");
            DropColumn("dbo.Accommodations", "Trip_Id");
            CreateIndex("dbo.Trips", "Restaurant_Id");
            CreateIndex("dbo.Trips", "Accommodation_Id");
            AddForeignKey("dbo.Trips", "Restaurant_Id", "dbo.Restaurants", "Id");
            AddForeignKey("dbo.Trips", "Accommodation_Id", "dbo.Accommodations", "Id");
        }
    }
}
