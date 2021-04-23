namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampusIdOnVenues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "CampusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Venues", "CampusId");
            AddForeignKey("dbo.Venues", "CampusId", "dbo.Campus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venues", "CampusId", "dbo.Campus");
            DropIndex("dbo.Venues", new[] { "CampusId" });
            DropColumn("dbo.Venues", "CampusId");
        }
    }
}
