using GraphQL.Data.EF;
using System.Linq.Expressions;

namespace GraphQL.Services.Core.Common
{
    public interface IBaseService<T> where T : EntityBase
    {
        ApplicationDbContext Context { get; }
        IQueryable<T> GetAsync();
        Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
