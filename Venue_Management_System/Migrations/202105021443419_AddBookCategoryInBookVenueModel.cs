namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCategoryInBookVenueModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookVenueCategories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        BookVenueType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BookVenues", "bookVenueCategoryId", c => c.Byte(nullable: false));
            CreateIndex("dbo.BookVenues", "bookVenueCategoryId");
            AddForeignKey("dbo.BookVenues", "bookVenueCategoryId", "dbo.BookVenueCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookVenues", "bookVenueCategoryId", "dbo.BookVenueCategories");
            DropIndex("dbo.BookVenues", new[] { "bookVenueCategoryId" });
            DropColumn("dbo.BookVenues", "bookVenueCategoryId");
            DropTable("dbo.BookVenueCategories");
        }
    }
}
