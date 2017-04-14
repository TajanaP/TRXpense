using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories.Base;

namespace TRXpense.Dal.Repositories
{
    public class CountryAllowanceRepository : BaseRepository<CountryAllowance>, ICountryAllowanceRepository
    {
        //public CountryAllowanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        //{
        //}
        public CountryAllowanceRepository() : base(new ApplicationDbContext())
        {
        }
    }
}
