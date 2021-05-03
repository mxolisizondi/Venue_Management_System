namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookIdToBorrowedBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BorrowedBooks", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.BorrowedBooks", "BookId");
            AddForeignKey("dbo.BorrowedBooks", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedBooks", "BookId", "dbo.Books");
            DropIndex("dbo.BorrowedBooks", new[] { "BookId" });
            DropColumn("dbo.BorrowedBooks", "BookId");
        }
    }
}
