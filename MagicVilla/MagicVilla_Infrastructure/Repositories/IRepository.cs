using MagicVilla_Infrastructure.Entities;
using System.Linq.Expressions;

namespace MagicVilla_Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task Add(TEntity entity);
        Task Remove(TKey id);
        Task Remove(TEntity entityToDelete);
        Task Remove(Expression<Func<TEntity, bool>> filter);
        Task Edit(TEntity entityToUpdate);
        Task<int> GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<IList<TEntity>> Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        Task<IList<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task<(IList<TEntity> data, int total, int totalDisplay)> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);

        Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamic(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);
    }
}
