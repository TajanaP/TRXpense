using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TRXpense.Bll.Model;

namespace TRXpense.Dal.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CostCenter> CostCenters { get; set; }

        // ApplicationUser override
        //public override IDbSet<ApplicationUser> Users { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
