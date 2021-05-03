namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentIdToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "departmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "departmentId");
            AddForeignKey("dbo.Students", "departmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "departmentId", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "departmentId" });
            DropColumn("dbo.Students", "departmentId");
        }
    }
}
