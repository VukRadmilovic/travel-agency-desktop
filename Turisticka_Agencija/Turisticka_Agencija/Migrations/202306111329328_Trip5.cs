namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trip5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripBoughtOrReservedByUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TripId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TripBoughtOrReservedByUsers");
        }
    }
}
