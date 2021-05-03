namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBorrowedStatusOnModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BorrowedBooks", "BorrowedStatusId", "dbo.BorrowedStatus");
            DropIndex("dbo.BorrowedBooks", new[] { "BorrowedStatusId" });
            DropColumn("dbo.BorrowedBooks", "BorrowedStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BorrowedBooks", "BorrowedStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.BorrowedBooks", "BorrowedStatusId");
            AddForeignKey("dbo.BorrowedBooks", "BorrowedStatusId", "dbo.BorrowedStatus", "Id", cascadeDelete: true);
        }
    }
}
