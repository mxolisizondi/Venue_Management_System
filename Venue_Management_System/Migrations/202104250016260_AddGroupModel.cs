namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Groups", new[] { "Student_UseId" });
            DropColumn("dbo.Groups", "UserId");
            RenameColumn(table: "dbo.BookVenues", name: "Student_UseId", newName: "UserId");
            RenameColumn(table: "dbo.Students", name: "UseId", newName: "UserId");
            RenameColumn(table: "dbo.BorrowedBooks", name: "Student_UseId", newName: "UserId");
            RenameColumn(table: "dbo.Events", name: "Student_UseId", newName: "UserId");
            RenameColumn(table: "dbo.Groups", name: "Student_UseId", newName: "UserId");
            RenameColumn(table: "dbo.Staffs", name: "UseId", newName: "UserId");
            RenameIndex(table: "dbo.BookVenues", name: "IX_Student_UseId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Students", name: "IX_UseId", newName: "IX_UserId");
            RenameIndex(table: "dbo.BorrowedBooks", name: "IX_Student_UseId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Events", name: "IX_Student_UseId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Staffs", name: "IX_UseId", newName: "IX_UserId");
            AlterColumn("dbo.Groups", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Groups", "UserId");
            DropColumn("dbo.BookVenues", "StudentNumber");
            DropColumn("dbo.BorrowedBooks", "StudentNumber");
            DropColumn("dbo.Events", "StudentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "StudentNumber", c => c.Long(nullable: false));
            AddColumn("dbo.BorrowedBooks", "StudentNumber", c => c.Long(nullable: false));
            AddColumn("dbo.BookVenues", "StudentNumber", c => c.Long(nullable: false));
            DropIndex("dbo.Groups", new[] { "UserId" });
            AlterColumn("dbo.Groups", "UserId", c => c.String());
            RenameIndex(table: "dbo.Staffs", name: "IX_UserId", newName: "IX_UseId");
            RenameIndex(table: "dbo.Events", name: "IX_UserId", newName: "IX_Student_UseId");
            RenameIndex(table: "dbo.BorrowedBooks", name: "IX_UserId", newName: "IX_Student_UseId");
            RenameIndex(table: "dbo.Students", name: "IX_UserId", newName: "IX_UseId");
            RenameIndex(table: "dbo.BookVenues", name: "IX_UserId", newName: "IX_Student_UseId");
            RenameColumn(table: "dbo.Staffs", name: "UserId", newName: "UseId");
            RenameColumn(table: "dbo.Groups", name: "UserId", newName: "Student_UseId");
            RenameColumn(table: "dbo.Events", name: "UserId", newName: "Student_UseId");
            RenameColumn(table: "dbo.BorrowedBooks", name: "UserId", newName: "Student_UseId");
            RenameColumn(table: "dbo.Students", name: "UserId", newName: "UseId");
            RenameColumn(table: "dbo.BookVenues", name: "UserId", newName: "Student_UseId");
            AddColumn("dbo.Groups", "UserId", c => c.String());
            CreateIndex("dbo.Groups", "Student_UseId");
        }
    }
}
