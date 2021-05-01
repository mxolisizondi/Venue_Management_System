namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBookStatus : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[BookStatus] ([Id], [Status]) VALUES (1, N'Active')
                    INSERT INTO [dbo].[BookStatus] ([Id], [Status]) VALUES (2, N'InActive')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
