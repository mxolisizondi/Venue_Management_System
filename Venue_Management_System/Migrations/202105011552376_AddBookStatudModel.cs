namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookStatudModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "BookStatusId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Books", "BookStatusId");
            AddForeignKey("dbo.Books", "BookStatusId", "dbo.BookStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BookStatusId", "dbo.BookStatus");
            DropIndex("dbo.Books", new[] { "BookStatusId" });
            DropColumn("dbo.Books", "BookStatusId");
            DropTable("dbo.BookStatus");
        }
    }
}
