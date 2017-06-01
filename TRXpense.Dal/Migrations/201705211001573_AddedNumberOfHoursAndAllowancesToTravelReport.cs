namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNumberOfHoursAndAllowancesToTravelReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelReports", "NumberOfHours", c => c.Double(nullable: false));
            AddColumn("dbo.TravelReports", "NumberOfAllowances", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TravelReports", "NumberOfAllowances");
            DropColumn("dbo.TravelReports", "NumberOfHours");
        }
    }
}
