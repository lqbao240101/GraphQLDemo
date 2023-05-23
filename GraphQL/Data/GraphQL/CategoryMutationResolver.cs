using GraphQL.Data.Entities;
using GraphQL.Services.Core.IServices;
using static GraphQL.ViewModels.Catalog.Entities;

namespace GraphQL.Data.GraphQL
{
    [ExtendObjectType("Mutation")]
    public class CategoryMutationResolver
    {
        [GraphQLName("createCategory")]
        [GraphQLDescription("Create New Category")]
        public async Task<Category> CreateCategoryAsync(CategoryRequest newCategory,
            [Service] ICategoryService categoryService)
        {
            try
            {
                var result = await categoryService.Add(newCategory);
                return result;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error($"{ex.Message}", "CATEGORY_CREATE_FAILED"));
            }
        }

        [GraphQLName("updateCategory")]
        [GraphQLDescription("Update Category")]
        public async Task<Category> UpdateCategoryAsync(Guid id, CategoryRequest updateCategory,
            [Service] ICategoryService categoryService)
        {
            try
            {
                var category = await categoryService.Update(updateCategory, id);
                return category;
            }
            catch (KeyNotFoundException k)
            {
                throw new GraphQLException(new Error($"{k.Message}", "CATEGORY_NOT_FOUND"));
            }
            catch (Exception e)
            {
                throw new GraphQLException(new Error($"{e.Message}", "CATEGORY_CREATE_FAILED"));
            }
        }

        [GraphQLName("deleteCategory")]
        [GraphQLDescription("Delete Category")]
        public async Task<Guid> DeleteCategoryAsync(Guid id,
            [Service] ICategoryService categoryService)
        {
            try
            {
                return await categoryService.Delete(id);
            }
            catch (KeyNotFoundException k)
            {
                throw new GraphQLException(new Error($"{k.Message}", "CATEGORY_NOT_FOUND"));
            }
            catch (Exception e)
            {
                throw new GraphQLException(new Error($"{e.Message}", "CATEGORY_DELETE_FAILED"));
            }
        }
    }
}