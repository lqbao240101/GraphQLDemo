using GraphQL.Data.Entities;
using static GraphQL.ViewModels.Catalog.Entities;

namespace GraphQL.Services.Core.IServices
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        Task<Product> Get(Guid id);
        Task<Product> Add(ProductRequest product);
        Task<Product> Update(ProductRequest product, Guid id);
        Task<Guid> Delete(Guid id);
    }
}
