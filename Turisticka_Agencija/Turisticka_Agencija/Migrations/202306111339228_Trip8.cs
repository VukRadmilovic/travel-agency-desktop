namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trip8 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TripBoughtOrReservedByUsers");
            AddPrimaryKey("dbo.TripBoughtOrReservedByUsers", new[] { "TripId", "UserId", "Action" });
            DropColumn("dbo.TripBoughtOrReservedByUsers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TripBoughtOrReservedByUsers", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TripBoughtOrReservedByUsers");
            AddPrimaryKey("dbo.TripBoughtOrReservedByUsers", "Id");
        }
    }
}
