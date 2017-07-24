namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOfficialCurrencyToCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountryAllowances", "AllowanceCurrency", c => c.String(nullable: false, maxLength: 3));
            AddColumn("dbo.CountryAllowances", "OfficialCurrency", c => c.String(nullable: false, maxLength: 3));
            DropColumn("dbo.CountryAllowances", "Currency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CountryAllowances", "Currency", c => c.String(nullable: false, maxLength: 3));
            DropColumn("dbo.CountryAllowances", "OfficialCurrency");
            DropColumn("dbo.CountryAllowances", "AllowanceCurrency");
        }
    }
}
