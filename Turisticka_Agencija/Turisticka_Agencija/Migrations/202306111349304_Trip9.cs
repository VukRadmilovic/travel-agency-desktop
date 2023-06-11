namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trip9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trips", "QuantitySold");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "QuantitySold", c => c.Int(nullable: false));
        }
    }
}
