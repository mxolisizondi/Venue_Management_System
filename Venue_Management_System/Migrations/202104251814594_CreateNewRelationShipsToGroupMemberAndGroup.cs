namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNewRelationShipsToGroupMemberAndGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "GroupMemberId", "dbo.GroupMembers");
            DropIndex("dbo.Groups", new[] { "GroupMemberId" });
            RenameColumn(table: "dbo.Groups", name: "UserId", newName: "Student_UserId");
            RenameIndex(table: "dbo.Groups", name: "IX_UserId", newName: "IX_Student_UserId");
            AddColumn("dbo.GroupMembers", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.GroupMembers", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Groups", "GroupLeaderId", c => c.String());
            CreateIndex("dbo.GroupMembers", "GroupId");
            CreateIndex("dbo.GroupMembers", "UserId");
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupMembers", "UserId", "dbo.Students", "UserId");
            DropColumn("dbo.GroupMembers", "StudentNumber");
            DropColumn("dbo.Groups", "GroupMemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "GroupMemberId", c => c.Int(nullable: false));
            AddColumn("dbo.GroupMembers", "StudentNumber", c => c.Long(nullable: false));
            DropForeignKey("dbo.GroupMembers", "UserId", "dbo.Students");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "UserId" });
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropColumn("dbo.Groups", "GroupLeaderId");
            DropColumn("dbo.GroupMembers", "UserId");
            DropColumn("dbo.GroupMembers", "GroupId");
            RenameIndex(table: "dbo.Groups", name: "IX_Student_UserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Groups", name: "Student_UserId", newName: "UserId");
            CreateIndex("dbo.Groups", "GroupMemberId");
            AddForeignKey("dbo.Groups", "GroupMemberId", "dbo.GroupMembers", "Id", cascadeDelete: true);
        }
    }
}
