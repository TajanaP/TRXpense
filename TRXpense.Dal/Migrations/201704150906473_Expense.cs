namespace TRXpense.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Expense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        BillNumber = c.String(nullable: false, maxLength: 50),
                        BillAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategories", t => t.ExpenseCategoryId, cascadeDelete: false)
                .Index(t => t.ExpenseCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "ExpenseCategoryId", "dbo.ExpenseCategories");
            DropIndex("dbo.Expenses", new[] { "ExpenseCategoryId" });
            DropTable("dbo.Expenses");
        }
    }
}
