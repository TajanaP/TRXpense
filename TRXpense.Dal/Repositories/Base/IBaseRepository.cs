using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TRXpense.Dal.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllFromDatabaseQueryable();
        IEnumerable<TEntity> GetAllFromDatabaseEnumerable();
        TEntity FindById(int id);
        TEntity FindById(string id);
        IEnumerable<TEntity> FindByExpressionInDatabase(Expression<Func<TEntity, bool>> predicate);
        void AddToDatabase(TEntity entity);
        void AddOrUpdateInDatabase(TEntity entity);
        void AddRangeToDatabase(IEnumerable<TEntity> entities);
        void UpdateInDatabase(TEntity entity);
        //void UpdateInDatabase(TEntity entity, Guid id);
        void UpdateInDatabase(TEntity entity, string id);
        void UpdateInDatabase(TEntity entity, int id);
        void DeleteFromDatabase(TEntity entity);
        void DeleteRangeFromDatabase(IEnumerable<TEntity> entities);
        void Save();
        Task SaveAsync();
    }
}
