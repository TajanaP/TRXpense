using System.Data.Entity;
using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories.Base;

namespace TRXpense.Dal.Repositories
{
    public class CostCenterRepository : BaseRepository<CostCenter>, ICostCenterRepository
    {
        //public CostCenterRepository(ApplicationDbContext dbContext) : base(dbContext)
        //{
        //}
        public CostCenterRepository() : base(new ApplicationDbContext())
        {
        }
    }
}
