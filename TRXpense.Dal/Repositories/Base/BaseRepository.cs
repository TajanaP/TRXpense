﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public TEntity FindById(string id)
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

        //public void UpdateInDatabase(TEntity entity, Guid id)
        //{
        //    var baseEntity = _dbContext.Set<TEntity>().Find(id);
        //    _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        //}

        public void UpdateInDatabase(TEntity entity, string id)
        {
            var baseEntity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Entry(baseEntity).CurrentValues.SetValues(entity);
        }

        public void UpdateInDatabase(TEntity entity, int id)
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

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
