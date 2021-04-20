namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'037db853-7f27-466d-9d8f-d266a5a4472e', N'21709920@dut4life.ac.za', 0, N'AOFilFwaRFo/Rh3Cjn8TsU+yQDvd5ukP1IpjKZJ0p5yRPesq4E6a3KpdBVX9AuKIGg==', N'c6baa04e-8cbf-414c-b944-482520e9823d', NULL, 0, 0, NULL, 1, 0, N'21709902@dut4life.ac.za')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'18a066a9-a235-4a2c-8f1a-9881f82f969b', N'mxolisizondi20@gmail.com', 0, N'ANWt4DZF5ZWa5NAJ1yJkiZxh1Z2gKIR/XqNVcjoQaO+H7nhulR1BV+rWxYTcvZVc9A==', N'731ff2a2-eb15-4855-bc88-2e47369a14f2', NULL, 0, 0, NULL, 1, 0, N'mxolisizondi20@gmail.com')


                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'fd43d5d6-7507-421e-806c-1d2339f4c5e8', N'Student')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0e3cf7c8-b62b-4eae-aec1-b5939d79df21', N'SystemAdmin')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'18a066a9-a235-4a2c-8f1a-9881f82f969b', N'0e3cf7c8-b62b-4eae-aec1-b5939d79df21')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'037db853-7f27-466d-9d8f-d266a5a4472e', N'fd43d5d6-7507-421e-806c-1d2339f4c5e8')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
