using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalSystem.Boundaries.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DentalSystem.Infrastructure.Common.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        public DbContext DbContext { get; }

        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> AsNoTracking()
        {
            return DbContext.Set<TEntity>().AsNoTracking();
        }

        public void Remove(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            DbContext.RemoveRange(entities);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> where)
        {
            return await DbContext.Set<TEntity>().SingleAsync(where);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> where)
        {
            return await DbContext.Set<TEntity>().SingleOrDefaultAsync(where);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            return DbContext.Set<TEntity>().Where(where);
        }
    }
}