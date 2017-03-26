using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories.Base;

namespace TRXpense.Dal.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        //public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext)
        //{
        //}
        public VehicleRepository() : base(new ApplicationDbContext())
        {
        }
    }
}
