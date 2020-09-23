using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalScheduler.Interfaces.Gateways;
using Microsoft.EntityFrameworkCore;

namespace DentalScheduler.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        public DbContext DbContext { get; }
        
        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return DbContext.Set<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            DbContext.RemoveRange(entities);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> where)
        {
            return DbContext.Set<TEntity>().Single(where);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return DbContext.Set<TEntity>().SingleOrDefault(where);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            return DbContext.Set<TEntity>().Where(where);
        }
    }
}