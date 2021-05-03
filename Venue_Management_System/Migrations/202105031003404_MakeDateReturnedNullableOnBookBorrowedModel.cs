namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDateReturnedNullableOnBookBorrowedModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BorrowedBooks", "DateReturned", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BorrowedBooks", "DateReturned", c => c.DateTime(nullable: false));
        }
    }
}
