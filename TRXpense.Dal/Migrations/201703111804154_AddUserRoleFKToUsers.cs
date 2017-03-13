namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRoleFKToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserRoleId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "UserRoleId");
            AddForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles", "Id");
            DropColumn("dbo.AspNetUsers", "UserRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "UserRoleId" });
            DropColumn("dbo.AspNetUsers", "UserRoleId");
        }
    }
}
