namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentIDToVenueModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "departmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Venues", "departmentId");
            AddForeignKey("dbo.Venues", "departmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venues", "departmentId", "dbo.Departments");
            DropIndex("dbo.Venues", new[] { "departmentId" });
            DropColumn("dbo.Venues", "departmentId");
        }
    }
}
