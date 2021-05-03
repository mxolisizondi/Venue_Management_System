namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupBookingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        bookVenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookVenues", t => t.bookVenueId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.bookVenueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupBookings", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupBookings", "bookVenueId", "dbo.BookVenues");
            DropIndex("dbo.GroupBookings", new[] { "bookVenueId" });
            DropIndex("dbo.GroupBookings", new[] { "GroupId" });
            DropTable("dbo.GroupBookings");
        }
    }
}
