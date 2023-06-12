namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ibegu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TripPlace", "PlaceId", "dbo.Places");
            DropIndex("dbo.TripPlace", new[] { "PlaceId" });
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.TripPlace");
            AlterColumn("dbo.Places", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TripPlace", "PlaceId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Places", "Name");
            AddPrimaryKey("dbo.TripPlace", new[] { "TripId", "PlaceId" });
            CreateIndex("dbo.TripPlace", "PlaceId");
            AddForeignKey("dbo.TripPlace", "PlaceId", "dbo.Places", "Name", cascadeDelete: true);
            DropColumn("dbo.Places", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Places", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TripPlace", "PlaceId", "dbo.Places");
            DropIndex("dbo.TripPlace", new[] { "PlaceId" });
            DropPrimaryKey("dbo.TripPlace");
            DropPrimaryKey("dbo.Places");
            AlterColumn("dbo.TripPlace", "PlaceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Places", "Name", c => c.String());
            AddPrimaryKey("dbo.TripPlace", new[] { "TripId", "PlaceId" });
            AddPrimaryKey("dbo.Places", "Id");
            CreateIndex("dbo.TripPlace", "PlaceId");
            AddForeignKey("dbo.TripPlace", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
    }
}
