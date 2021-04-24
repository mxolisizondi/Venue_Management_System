namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                        StudentNumber = c.Long(nullable: false),
                        Student_UseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_UseId)
                .Index(t => t.Student_UseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "Student_UseId", "dbo.Students");
            DropIndex("dbo.Groups", new[] { "Student_UseId" });
            DropTable("dbo.Groups");
        }
    }
}
