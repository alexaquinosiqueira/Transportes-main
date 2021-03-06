using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Transportadora.Business.Models;
using X.PagedList;

namespace Transportadora.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetPaged(int pageIndex, int pageSize, int totalCount, IList<Expression<Func<TEntity, bool>>> wheres = null, Expression<Func<TEntity, object>> orderBy = null, bool ascending = true);
        Task<IPagedList<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetById(long id);
        Task<TEntity> GetByIdDetached(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}