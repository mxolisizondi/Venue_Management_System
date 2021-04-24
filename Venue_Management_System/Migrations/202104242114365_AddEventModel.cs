namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Color = c.String(),
                        IsFull = c.Byte(nullable: false),
                        StudentNumber = c.Long(nullable: false),
                        Student_UseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_UseId)
                .Index(t => t.Student_UseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Student_UseId", "dbo.Students");
            DropIndex("dbo.Events", new[] { "Student_UseId" });
            DropTable("dbo.Events");
        }
    }
}
