namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehiclesProductionYearToInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "ProductionYear", c => c.Int(nullable: true));
            DropColumn("dbo.Vehicles", "ManufactureYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "ManufactureYear", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vehicles", "ProductionYear");
        }
    }
}
