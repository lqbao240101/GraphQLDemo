using GraphQL.Data.Entities;
using static GraphQL.ViewModels.Catalog.Entities;

namespace GraphQL.Services.Core.IServices
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        Task<Category> Get(Guid id);
        Task<Category> Add(CategoryRequest category);
        Task<Category> Update(CategoryRequest category, Guid i);
        Task<Guid> Delete(Guid id);
    }
}
