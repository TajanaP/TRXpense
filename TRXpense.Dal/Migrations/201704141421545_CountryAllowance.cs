namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryAllowance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryAllowances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CountryAllowances");
        }
    }
}
