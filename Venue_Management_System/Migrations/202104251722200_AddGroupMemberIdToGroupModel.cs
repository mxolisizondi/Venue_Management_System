namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupMemberIdToGroupModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            AddColumn("dbo.Groups", "GroupMemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Groups", "GroupMemberId");
            AddForeignKey("dbo.Groups", "GroupMemberId", "dbo.GroupMembers", "Id", cascadeDelete: true);
            DropColumn("dbo.GroupMembers", "GroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupMembers", "GroupId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Groups", "GroupMemberId", "dbo.GroupMembers");
            DropIndex("dbo.Groups", new[] { "GroupMemberId" });
            DropColumn("dbo.Groups", "GroupMemberId");
            CreateIndex("dbo.GroupMembers", "GroupId");
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
        }
    }
}
