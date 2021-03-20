using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DentalSystem.Interfaces.Infrastructure.Common.Persistence
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> where);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> AsQueryable();

        IQueryable<TEntity> AsNoTracking();

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where);

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        void Remove(IEnumerable<TEntity> entities);
    }
}