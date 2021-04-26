namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropRelationshipOnGroup : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Groups", name: "Student_UserId", newName: "UserId");
            RenameIndex(table: "dbo.Groups", name: "IX_Student_UserId", newName: "IX_UserId");
            DropColumn("dbo.Groups", "GroupLeaderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "GroupLeaderId", c => c.String());
            RenameIndex(table: "dbo.Groups", name: "IX_UserId", newName: "IX_Student_UserId");
            RenameColumn(table: "dbo.Groups", name: "UserId", newName: "Student_UserId");
        }
    }
}
