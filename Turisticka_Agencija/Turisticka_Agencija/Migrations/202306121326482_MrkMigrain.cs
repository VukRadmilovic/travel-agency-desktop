namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MrkMigrain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trips", "StartLatitude");
            DropColumn("dbo.Trips", "StartLongitude");
            DropColumn("dbo.Trips", "EndLatitude");
            DropColumn("dbo.Trips", "EndLongitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "EndLongitude", c => c.Double(nullable: false));
            AddColumn("dbo.Trips", "EndLatitude", c => c.Double(nullable: false));
            AddColumn("dbo.Trips", "StartLongitude", c => c.Double(nullable: false));
            AddColumn("dbo.Trips", "StartLatitude", c => c.Double(nullable: false));
        }
    }
}
