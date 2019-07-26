using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CareebizExam.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
    
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Add(params TEntity[] entities);
        void Add(IEnumerable<TEntity> entities);


        void Delete(TEntity entity);
        void Delete(params TEntity[] entities);
        void Delete(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(params TEntity[] entities);
        void Update(IEnumerable<TEntity> entities);



    }

}
