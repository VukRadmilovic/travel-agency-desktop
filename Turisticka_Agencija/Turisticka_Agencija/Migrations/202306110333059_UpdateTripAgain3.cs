namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTripAgain3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Trip_Id", c => c.Int());
            AddColumn("dbo.Users", "Trip_Id1", c => c.Int());
            CreateIndex("dbo.Users", "Trip_Id");
            CreateIndex("dbo.Users", "Trip_Id1");
            AddForeignKey("dbo.Users", "Trip_Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.Users", "Trip_Id1", "dbo.Trips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Trip_Id1", "dbo.Trips");
            DropForeignKey("dbo.Users", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Users", new[] { "Trip_Id1" });
            DropIndex("dbo.Users", new[] { "Trip_Id" });
            DropColumn("dbo.Users", "Trip_Id1");
            DropColumn("dbo.Users", "Trip_Id");
        }
    }
}
