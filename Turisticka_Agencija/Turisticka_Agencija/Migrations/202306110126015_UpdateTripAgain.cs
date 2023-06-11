namespace Turisticka_Agencija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTripAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "User_Id", c => c.Int());
            AddColumn("dbo.Trips", "User_Id1", c => c.Int());
            CreateIndex("dbo.Trips", "User_Id");
            CreateIndex("dbo.Trips", "User_Id1");
            AddForeignKey("dbo.Trips", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Trips", "User_Id1", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Trips", "User_Id", "dbo.Users");
            DropIndex("dbo.Trips", new[] { "User_Id1" });
            DropIndex("dbo.Trips", new[] { "User_Id" });
            DropColumn("dbo.Trips", "User_Id1");
            DropColumn("dbo.Trips", "User_Id");
        }
    }
}
