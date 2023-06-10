namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTransportToDescriptionForTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "Description", c => c.String());
            DropColumn("dbo.Trips", "Transport");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "Transport", c => c.String());
            DropColumn("dbo.Trips", "Description");
        }
    }
}
