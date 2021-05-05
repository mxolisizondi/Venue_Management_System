namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnotationsOnTheBookVenueModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookVenues", "BookingDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookVenues", "BookingDate", c => c.DateTime(nullable: false));
        }
    }
}
