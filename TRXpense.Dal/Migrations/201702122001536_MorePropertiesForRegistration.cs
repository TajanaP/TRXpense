namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MorePropertiesForRegistration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "OIB", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.AspNetUsers", "CostCenter", c => c.String(nullable: false, maxLength: 8));
            AddColumn("dbo.AspNetUsers", "Position", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Position");
            DropColumn("dbo.AspNetUsers", "CostCenter");
            DropColumn("dbo.AspNetUsers", "OIB");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "UserRole");
        }
    }
}
