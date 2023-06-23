using System.Linq.Expressions;

namespace DayOne.API.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T entity);
        Task DeleteAsync();
    }
   
}
