namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMileageToTravelReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelReports", "StartMileage", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TravelReports", "EndMileage", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TravelReports", "EndMileage");
            DropColumn("dbo.TravelReports", "StartMileage");
        }
    }
}
