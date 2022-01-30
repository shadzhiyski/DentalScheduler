using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DentalSystem.Infrastructure.Common.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IReadRepository<TEntity>, IWriteRepository<TEntity>
        where TEntity : class
    {
        public DbContext DbContext { get; }

        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken)
            => DbContext.AddAsync(entity, cancellationToken).AsTask();

        public IQueryable<TEntity> AsQueryable() => DbContext.Set<TEntity>();

        public IQueryable<TEntity> AsNoTracking() => DbContext.Set<TEntity>().AsNoTracking();

        public void Remove(TEntity entity) => DbContext.Remove(entity);

        public void Remove(IEnumerable<TEntity> entities) => DbContext.RemoveRange(entities);

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken)
            => DbContext.Set<TEntity>().SingleAsync(where, cancellationToken);

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken)
            => DbContext.Set<TEntity>().SingleOrDefaultAsync(where, cancellationToken);

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where) => DbContext.Set<TEntity>().Where(where);
    }
}