namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUserRoleFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "UserRoleId" });
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserRoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserRoleId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUsers", "UserRole");
            CreateIndex("dbo.AspNetUsers", "UserRoleId");
            AddForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
