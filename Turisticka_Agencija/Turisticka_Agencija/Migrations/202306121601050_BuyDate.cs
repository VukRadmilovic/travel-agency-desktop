namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TripBoughtOrReservedByUsers", "BuyDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TripBoughtOrReservedByUsers", "BuyDate");
        }
    }
}
