namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCostCenterDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CostCenters", "Description", c => c.String(nullable: false));
            DropColumn("dbo.CostCenters", "SuperiorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CostCenters", "SuperiorId", c => c.Int(nullable: false));
            DropColumn("dbo.CostCenters", "Description");
        }
    }
}
