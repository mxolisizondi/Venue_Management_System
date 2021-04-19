namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "Address");
        }
    }
}
