namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDataToVenueStatus : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[VenueStatus] ([Id], [Status]) VALUES (1, N'Active')
                    INSERT INTO [dbo].[VenueStatus] ([Id], [Status]) VALUES (2, N'InActive')
"
                );
        }
        
        public override void Down()
        {
        }
    }
}
