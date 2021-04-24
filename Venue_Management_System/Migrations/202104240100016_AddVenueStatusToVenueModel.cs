namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVenueStatusToVenueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "venueStatusId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Venues", "venueStatusId");
            AddForeignKey("dbo.Venues", "venueStatusId", "dbo.VenueStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venues", "venueStatusId", "dbo.VenueStatus");
            DropIndex("dbo.Venues", new[] { "venueStatusId" });
            DropColumn("dbo.Venues", "venueStatusId");
        }
    }
}
