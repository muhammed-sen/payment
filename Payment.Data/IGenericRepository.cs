using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Payment.Data.Domain;

namespace Payment.Data
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            StringValues includeProperties);

        Task Insert(TEntity entity);
        void Update(TEntity entityToUpdate);

    }
}
