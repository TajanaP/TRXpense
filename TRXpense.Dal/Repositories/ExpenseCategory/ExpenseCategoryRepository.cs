using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories.Base;

namespace TRXpense.Dal.Repositories
{
    public class ExpenseCategoryRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        //public ExpenseCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        //{
        //}
        public ExpenseCategoryRepository() : base(new ApplicationDbContext())
        {
        }
    }
}
