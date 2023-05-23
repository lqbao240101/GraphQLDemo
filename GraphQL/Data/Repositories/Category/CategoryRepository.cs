using GraphQL.Data.EF;
using GraphQL.Services.Core.Common;

namespace GraphQL.Data.Repositories.Category
{
    public interface ICategoryRepository : IBaseService<Entities.Category>
    {
    }

    public class CategoryRepository : BaseService<Entities.Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
