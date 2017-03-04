namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CostCenterId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CostCenterId");
            AddForeignKey("dbo.AspNetUsers", "CostCenterId", "dbo.CostCenters", "Id", cascadeDelete: false);
            DropColumn("dbo.AspNetUsers", "CostCenter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CostCenter", c => c.String(nullable: false, maxLength: 8));
            DropForeignKey("dbo.AspNetUsers", "CostCenterId", "dbo.CostCenters");
            DropIndex("dbo.AspNetUsers", new[] { "CostCenterId" });
            DropColumn("dbo.AspNetUsers", "CostCenterId");
        }
    }
}
