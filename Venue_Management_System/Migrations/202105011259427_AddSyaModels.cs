namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSyaModels : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.BorrowHistoryId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_UserId)
                .ForeignKey("dbo.Book_Sya", t => t.Book_Sya_Id)
                .Index(t => t.BookId)
                .Index(t => t.Student_UserId)
                .Index(t => t.Book_Sya_Id);
            
            CreateTable(
                "dbo.BorrowedStatus_Sya",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowHistories", "Book_Sya_Id", "dbo.Book_Sya");
            DropForeignKey("dbo.BorrowHistories", "Student_UserId", "dbo.Students");
            DropForeignKey("dbo.BorrowHistories", "BookId", "dbo.Books");
            DropIndex("dbo.BorrowHistories", new[] { "Book_Sya_Id" });
            DropIndex("dbo.BorrowHistories", new[] { "Student_UserId" });
            DropIndex("dbo.BorrowHistories", new[] { "BookId" });
            DropTable("dbo.BorrowedStatus_Sya");
            DropTable("dbo.BorrowHistories");
            DropTable("dbo.Book_Sya");
        }
    }
}
