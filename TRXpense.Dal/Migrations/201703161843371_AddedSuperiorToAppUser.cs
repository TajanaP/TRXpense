namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSuperiorToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SuperiorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "SuperiorId");
            AddForeignKey("dbo.AspNetUsers", "SuperiorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "SuperiorId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "SuperiorId" });
            DropColumn("dbo.AspNetUsers", "SuperiorId");
        }
    }
}
