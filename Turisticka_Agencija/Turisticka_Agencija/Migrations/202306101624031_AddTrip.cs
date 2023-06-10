namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartLatitude = c.Double(nullable: false),
                        StartLongitude = c.Double(nullable: false),
                        EndLatitude = c.Double(nullable: false),
                        EndLongitude = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Transport = c.String(),
                        Accommodation_Id = c.Int(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accommodations", t => t.Accommodation_Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Accommodation_Id)
                .Index(t => t.Restaurant_Id);
            
            AddColumn("dbo.Places", "Trip_Id", c => c.Int());
            CreateIndex("dbo.Places", "Trip_Id");
            AddForeignKey("dbo.Places", "Trip_Id", "dbo.Trips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Places", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Trips", "Accommodation_Id", "dbo.Accommodations");
            DropIndex("dbo.Trips", new[] { "Restaurant_Id" });
            DropIndex("dbo.Trips", new[] { "Accommodation_Id" });
            DropIndex("dbo.Places", new[] { "Trip_Id" });
            DropColumn("dbo.Places", "Trip_Id");
            DropTable("dbo.Trips");
        }
    }
}
