using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Payment.Data.Context;

namespace Payment.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly PaymentContext _paymentContext;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(PaymentContext context)
        {
            _paymentContext = context;
            _dbSet = context.Set<TEntity>();
        }

        public async virtual Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public virtual void Update(TEntity entityToUpdate)
        {

            _dbSet.Attach(entityToUpdate);
            _paymentContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, StringValues includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await (orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync());
        }
    }
}