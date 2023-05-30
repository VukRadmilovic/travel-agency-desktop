namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Drugamigracija : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accommodations", "Address", c => c.String());
            AddColumn("dbo.Places", "Address", c => c.String());
            AddColumn("dbo.Restaurants", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Address");
            DropColumn("dbo.Places", "Address");
            DropColumn("dbo.Accommodations", "Address");
        }
    }
}
