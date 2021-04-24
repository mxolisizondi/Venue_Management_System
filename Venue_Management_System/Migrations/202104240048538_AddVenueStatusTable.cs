namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVenueStatusTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VenueStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VenueStatus");
        }
    }
}
