namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryAllowanceStringSizeRestriction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CountryAllowances", "Country", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CountryAllowances", "Currency", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CountryAllowances", "Currency", c => c.String(nullable: false));
            AlterColumn("dbo.CountryAllowances", "Country", c => c.String(nullable: false));
        }
    }
}
