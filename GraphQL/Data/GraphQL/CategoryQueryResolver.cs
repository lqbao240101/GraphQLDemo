using GraphQL.Data.Entities;
using GraphQL.Services.Core.IServices;

namespace GraphQL.Data.GraphQL
{
    [ExtendObjectType("Query")]
    public class CategoryQueryResolver
    {
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories([Service] ICategoryService categoryService)
        {
            return categoryService.GetAll();
        }
    }
}
