namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDepositAndNumberOfMealsToTravelReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelReports", "NumberOfMeals", c => c.Int(nullable: false));
            AddColumn("dbo.TravelReports", "Deposit", c => c.Boolean(nullable: false));
            AddColumn("dbo.TravelReports", "DepositAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TravelReports", "DepositAmount");
            DropColumn("dbo.TravelReports", "Deposit");
            DropColumn("dbo.TravelReports", "NumberOfMeals");
        }
    }
}
