namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRoleFKToUsers1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "UserRoleId" });
            AlterColumn("dbo.AspNetUsers", "UserRoleId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "UserRoleId");
            AddForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "UserRoleId" });
            AlterColumn("dbo.AspNetUsers", "UserRoleId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "UserRoleId");
            AddForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.AspNetRoles", "Id");
        }
    }
}
