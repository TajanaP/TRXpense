using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories.Base;

namespace TRXpense.Dal.Repositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        //public ExpenseRepository(ApplicationDbContext dbContext) : base(dbContext)
        //{
        //}
        public ExpenseRepository() : base(new ApplicationDbContext())
        {
        }
    }
}
