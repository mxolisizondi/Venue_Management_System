namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavigationPropertyToVenueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "VenueTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Venues", "VenueTypeId");
            AddForeignKey("dbo.Venues", "VenueTypeId", "dbo.VenueTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venues", "VenueTypeId", "dbo.VenueTypes");
            DropIndex("dbo.Venues", new[] { "VenueTypeId" });
            DropColumn("dbo.Venues", "VenueTypeId");
        }
    }
}
