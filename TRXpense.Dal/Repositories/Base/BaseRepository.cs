using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TRXpense.Dal.Database;

namespace TRXpense.Dal.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAllFromDatabaseEnumerable() => _dbContext.Set<TEntity>().AsEnumerable();

        public IQueryable<TEntity> GetAllFromDatabaseQueryable() => _dbContext.Set<TEntity>().AsQueryable();

        public IEnumerable<TEntity> FindByExpressionInDatabase(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsEnumerable();
        }

        public TEntity FindById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void AddToDatabase(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddOrUpdateInDatabase(TEntity entity)
        {
            TEntity baseEntity = _dbContext.Set<TEntity>().Find(entity);
            if (baseEntity == null)
                AddToDatabase(entity);
            else
                UpdateInDatabase(entity);
        }

        public void AddRangeToDatabase(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public void UpdateInDatabase(TEntity entity)
        {
            var baseEntity = _dbContext.Set<TEntity>().Find(entity);
            _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }

        public void UpdateInDatabase(TEntity entity, Guid id)
        {
            var baseEntity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }

        public void DeleteFromDatabase(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRangeFromDatabase(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void ExecuteUpdates()
        {
            _dbContext.SaveChanges();
        }

        public async Task ExecuteUpdatesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
