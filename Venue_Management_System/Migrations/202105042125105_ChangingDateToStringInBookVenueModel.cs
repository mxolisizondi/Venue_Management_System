namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingDateToStringInBookVenueModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookVenues", "BookingDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookVenues", "BookingDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
