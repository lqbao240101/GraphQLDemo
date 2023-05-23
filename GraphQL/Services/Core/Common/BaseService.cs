using GraphQL.Data.EF;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraphQL.Services.Core.Common
{
    public class BaseService<T> : IBaseService<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _context;

        public BaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext Context 
        {
            get { return _context; }
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? predicate)
        {
            IQueryable<T> query = _context.Set<T>();

            return predicate is null
                ? await query.FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAsync() => _context.Set<T>().OrderBy(_ => _.Id).AsQueryable();
        public async Task<T> AddAsync(T entity)
        {
            var added = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var updated = _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            return await _context.SaveChangesAsync() != 0;
        }
    }
}