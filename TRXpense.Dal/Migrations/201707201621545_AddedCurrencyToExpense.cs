namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCurrencyToExpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Currency", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "Currency");
        }
    }
}
