using System.Linq.Expressions;

namespace DataAccess.Repositories.Intefaces
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
        void Update(T entity);
        Task UpdateAsync(T entity);
    }
}