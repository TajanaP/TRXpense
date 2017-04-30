using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories.Base;

namespace TRXpense.Dal.Repositories
{
    public class TravelReportRepository : BaseRepository<TravelReport>, ITravelReportRepository
    {
        //public TravelReportRepository(ApplicationDbContext dbContext) : base(dbContext)
        //{
        //}
        public TravelReportRepository() : base(new ApplicationDbContext())
        {

        }
    }
}
