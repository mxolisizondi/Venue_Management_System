namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropBookStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "BookStatusId");
            DropTable("dbo.BookStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "BookStatusId", c => c.Int(nullable: false));
        }
    }
}
