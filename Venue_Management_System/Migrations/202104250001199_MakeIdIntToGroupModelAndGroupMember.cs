namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeIdIntToGroupModelAndGroupMember : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropPrimaryKey("dbo.GroupMembers");
            DropPrimaryKey("dbo.Groups");
            AlterColumn("dbo.GroupMembers", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.GroupMembers", "GroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.Groups", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.GroupMembers", "Id");
            AddPrimaryKey("dbo.Groups", "Id");
            CreateIndex("dbo.GroupMembers", "GroupId");
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropPrimaryKey("dbo.Groups");
            DropPrimaryKey("dbo.GroupMembers");
            AlterColumn("dbo.Groups", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.GroupMembers", "GroupId", c => c.Byte(nullable: false));
            AlterColumn("dbo.GroupMembers", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Groups", "Id");
            AddPrimaryKey("dbo.GroupMembers", "Id");
            CreateIndex("dbo.GroupMembers", "GroupId");
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
        }
    }
}
