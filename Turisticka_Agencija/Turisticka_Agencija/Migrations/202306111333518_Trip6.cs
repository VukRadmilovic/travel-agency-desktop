namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trip6 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TripBoughtOrReservedByUsers");
            AlterColumn("dbo.TripBoughtOrReservedByUsers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TripBoughtOrReservedByUsers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TripBoughtOrReservedByUsers");
            AlterColumn("dbo.TripBoughtOrReservedByUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TripBoughtOrReservedByUsers", "Id");
        }
    }
}
