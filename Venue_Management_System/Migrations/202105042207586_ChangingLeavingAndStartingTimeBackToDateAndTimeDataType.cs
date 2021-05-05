namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingLeavingAndStartingTimeBackToDateAndTimeDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookVenues", "StartingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BookVenues", "LeavingTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookVenues", "LeavingTime", c => c.String());
            AlterColumn("dbo.BookVenues", "StartingTime", c => c.String());
        }
    }
}
