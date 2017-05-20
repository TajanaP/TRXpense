namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExpensesToTravelReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "TravelReportId", c => c.Int(nullable: false));
            CreateIndex("dbo.Expenses", "TravelReportId");
            AddForeignKey("dbo.Expenses", "TravelReportId", "dbo.TravelReports", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "TravelReportId", "dbo.TravelReports");
            DropIndex("dbo.Expenses", new[] { "TravelReportId" });
            DropColumn("dbo.Expenses", "TravelReportId");
        }
    }
}
