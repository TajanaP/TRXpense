namespace TRXpense.Dal.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Vehicles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Brand = c.String(nullable: false),
                    Model = c.String(),
                    ManufactureYear = c.DateTime(nullable: true),
                    Registration = c.String(nullable: false, maxLength: 15),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}
