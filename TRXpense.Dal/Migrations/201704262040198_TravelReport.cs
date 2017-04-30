namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TravelReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TravelReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        CountryAllowanceId = c.Int(nullable: false),
                        City = c.String(maxLength: 50),
                        VehicleType = c.Int(nullable: false),
                        VehicleId = c.Int(),
                        ReasonForTravel = c.String(),
                        Departure = c.DateTime(nullable: false),
                        Return = c.DateTime(nullable: false),
                        ExpenseSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId)
                .ForeignKey("dbo.CountryAllowances", t => t.CountryAllowanceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.CountryAllowanceId)
                .Index(t => t.VehicleId);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TravelOrders",
                c => new
                    {
                        Idd = c.Int(nullable: false, identity: true),
                        Id = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        CountryAllowanceId = c.Int(nullable: false),
                        City = c.String(maxLength: 50),
                        VehicleType = c.Int(nullable: false),
                        VehicleId = c.Int(),
                        ReasonForTravel = c.String(),
                        Departure = c.DateTime(nullable: false),
                        Return = c.DateTime(nullable: false),
                        ExpenseSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Idd);
            
            DropForeignKey("dbo.TravelReports", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TravelReports", "CountryAllowanceId", "dbo.CountryAllowances");
            DropForeignKey("dbo.TravelReports", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.TravelReports", new[] { "VehicleId" });
            DropIndex("dbo.TravelReports", new[] { "CountryAllowanceId" });
            DropIndex("dbo.TravelReports", new[] { "EmployeeId" });
            DropTable("dbo.TravelReports");
        }
    }
}
