using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DentalScheduler.Interfaces.Infrastructure.Persistence
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Single(Expression<Func<TEntity, bool>> where);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> AsQueryable();

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Remove(IEnumerable<TEntity> entities);
    }
}