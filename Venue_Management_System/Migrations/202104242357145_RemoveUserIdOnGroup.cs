namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserIdOnGroup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "StudentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "StudentNumber", c => c.Long(nullable: false));
        }
    }
}
