namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGroupAnGroupMember : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "UserId", "dbo.Students");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "UserId" });
            DropTable("dbo.GroupMembers");
            DropTable("dbo.Groups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentNumber = c.Long(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Groups", "UserId");
            CreateIndex("dbo.GroupMembers", "GroupId");
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Groups", "UserId", "dbo.Students", "UserId");
        }
    }
}
