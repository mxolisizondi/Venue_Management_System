namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BorrowHistories", "BookId", "dbo.Books");
            DropForeignKey("dbo.BorrowHistories", "Student_UserId", "dbo.Students");
            DropForeignKey("dbo.BorrowHistories", "Book_Sya_Id", "dbo.Book_Sya");
            DropIndex("dbo.BorrowHistories", new[] { "BookId" });
            DropIndex("dbo.BorrowHistories", new[] { "Student_UserId" });
            DropIndex("dbo.BorrowHistories", new[] { "Book_Sya_Id" });
            DropTable("dbo.Book_Sya");
            DropTable("dbo.BorrowHistories");
            DropTable("dbo.BorrowedStatus_Sya");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BorrowedStatus_Sya",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BorrowHistories",
                c => new
                    {
                        BorrowHistoryId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Student_UserId = c.String(maxLength: 128),
                        Book_Sya_Id = c.Int(),
                    })
                .PrimaryKey(t => t.BorrowHistoryId);
            
            CreateTable(
                "dbo.Book_Sya",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                        Publisher = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BorrowHistories", "Book_Sya_Id");
            CreateIndex("dbo.BorrowHistories", "Student_UserId");
            CreateIndex("dbo.BorrowHistories", "BookId");
            AddForeignKey("dbo.BorrowHistories", "Book_Sya_Id", "dbo.Book_Sya", "Id");
            AddForeignKey("dbo.BorrowHistories", "Student_UserId", "dbo.Students", "UserId");
            AddForeignKey("dbo.BorrowHistories", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
