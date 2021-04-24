namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        DatePublished = c.DateTime(nullable: false),
                        TotalBooks = c.Int(nullable: false),
                        TotalBooksAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookVenues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentNumber = c.Long(nullable: false),
                        VenueId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        StartingTime = c.DateTime(nullable: false),
                        LeavingTime = c.DateTime(nullable: false),
                        Student_UseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_UseId)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId)
                .Index(t => t.Student_UseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        UseId = c.String(nullable: false, maxLength: 128),
                        StudentNumber = c.Long(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        CellNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UseId)
                .ForeignKey("dbo.AspNetUsers", t => t.UseId)
                .Index(t => t.UseId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfSits = c.Int(nullable: false),
                        NumberOfSitsAllowed = c.Int(nullable: false),
                        NumberOfSitsAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BorrowedBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentNumber = c.Long(nullable: false),
                        DateBorrowed = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        BorrowedStatusId = c.Int(nullable: false),
                        Student_UseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BorrowedStatus", t => t.BorrowedStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_UseId)
                .Index(t => t.BorrowedStatusId)
                .Index(t => t.Student_UseId);
            
            CreateTable(
                "dbo.BorrowedStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Campus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        UseId = c.String(nullable: false, maxLength: 128),
                        StaffNumber = c.Long(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        CellNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UseId)
                .ForeignKey("dbo.AspNetUsers", t => t.UseId)
                .Index(t => t.UseId);


            CreateTable(
               "dbo.Events",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   tittle = c.String(nullable: false, maxLength: 50),
                   Desc = c.String(nullable: false, maxLength: 128),
                   StartDate = c.DateTime(nullable: false),
                   EndDate = c.DateTime(nullable: false),
                   ThemeColor = c.String(nullable: false),
                   IsFullDay = c.Boolean(nullable: false),

               })
               .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VenueTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfVenue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "UseId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BorrowedBooks", "Student_UseId", "dbo.Students");
            DropForeignKey("dbo.BorrowedBooks", "BorrowedStatusId", "dbo.BorrowedStatus");
            DropForeignKey("dbo.BookVenues", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.BookVenues", "Student_UseId", "dbo.Students");
            DropForeignKey("dbo.Students", "UseId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Staffs", new[] { "UseId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.BorrowedBooks", new[] { "Student_UseId" });
            DropIndex("dbo.BorrowedBooks", new[] { "BorrowedStatusId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Students", new[] { "UseId" });
            DropIndex("dbo.BookVenues", new[] { "Student_UseId" });
            DropIndex("dbo.BookVenues", new[] { "VenueId" });
            DropTable("dbo.VenueTypes");
            DropTable("dbo.Staffs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Departments");
            DropTable("dbo.Campus");
            DropTable("dbo.BorrowedStatus");
            DropTable("dbo.BorrowedBooks");
            DropTable("dbo.Venues");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Students");
            DropTable("dbo.BookVenues");
            DropTable("dbo.Books");
        }
    }
}
