using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CareebizExam.Infrastructure
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();

        }
        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Add(params TEntity[] entities) => _dbSet.AddRange(entities);


        public void Add(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);


        public void Delete(TEntity entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }

        
        public void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }


        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

    }

}
