namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStartAndEndDateTimeToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trips", "End", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "End");
            DropColumn("dbo.Trips", "Start");
        }
    }
}
