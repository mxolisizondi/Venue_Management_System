namespace Venue_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodivModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CovidDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Data = c.Binary(),
                        UserId = c.String(maxLength: 128),
                        reportCaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReportCases", t => t.reportCaseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.reportCaseId);
            
            CreateTable(
                "dbo.ReportCases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        covidStatusId = c.Byte(nullable: false),
                        TestDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CovidStatus", t => t.covidStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.covidStatusId);
            
            CreateTable(
                "dbo.CovidStatus",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CovidDocuments", "UserId", "dbo.Students");
            DropForeignKey("dbo.ReportCases", "UserId", "dbo.Students");
            DropForeignKey("dbo.ReportCases", "covidStatusId", "dbo.CovidStatus");
            DropForeignKey("dbo.CovidDocuments", "reportCaseId", "dbo.ReportCases");
            DropIndex("dbo.ReportCases", new[] { "covidStatusId" });
            DropIndex("dbo.ReportCases", new[] { "UserId" });
            DropIndex("dbo.CovidDocuments", new[] { "reportCaseId" });
            DropIndex("dbo.CovidDocuments", new[] { "UserId" });
            DropTable("dbo.CovidStatus");
            DropTable("dbo.ReportCases");
            DropTable("dbo.CovidDocuments");
        }
    }
}
