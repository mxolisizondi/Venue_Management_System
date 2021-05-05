namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCovidStatus : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[CovidStatus] ([Id], [Status]) VALUES (1, N'Positive')
                    INSERT INTO [dbo].[CovidStatus] ([Id], [Status]) VALUES (2, N'Negative')

               ");
        }
        
        public override void Down()
        {
        }
    }
}
