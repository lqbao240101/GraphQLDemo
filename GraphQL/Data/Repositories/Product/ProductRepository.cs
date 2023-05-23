using GraphQL.Data.EF;
using GraphQL.Services.Core.Common;

namespace GraphQL.Data.Repositories.Product
{
    public interface IProductRepository : IBaseService<Entities.Product>
    {
    }

    public class ProductRepository : BaseService<Entities.Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
    }
}
