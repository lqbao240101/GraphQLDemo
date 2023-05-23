using GraphQL.Data.Entities;
using GraphQL.Services.Core.IServices;
using static GraphQL.ViewModels.Catalog.Entities;

namespace GraphQL.Data.GraphQL
{
    [ExtendObjectType("Mutation")]
    public class ProductMutationResolver
    {
        [GraphQLName("createProduct")]
        [GraphQLDescription("Create New Product")]
        public async Task<Product> CreateProductAsync(ProductRequest newProduct,
            [Service] IProductService productService)
        {
            try
            {
                var product = await productService.Add(newProduct);
                return product;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error($"{ex.Message}", "PRODUCT_CREATE_FAILED"));
            }
        }

        [GraphQLName("updateProduct")]
        [GraphQLDescription("Update Product")]
        public async Task<Product> UpdateProductAsync(Guid id, ProductRequest updateProduct,
            [Service] IProductService productService)
        {
            try
            {
                var product = await productService.Update(updateProduct, id);
                return product;
            }
            catch (KeyNotFoundException k)
            {
                throw new GraphQLException(new Error($"{k.Message}", "PRODUCT_NOT_FOUND"));
            }
            catch (Exception e)
            {
                throw new GraphQLException(new Error($"{e.Message}", "PRODUCT_CREATE_FAILED"));
            }
        }

        [GraphQLName("deleteProduct")]
        [GraphQLDescription("Delete Product")]
        public async Task<Guid> DeleteProductAsync(Guid id,
            [Service] IProductService productService)
        {
            try
            {
                return await productService.Delete(id);
            }
            catch (KeyNotFoundException k)
            {
                throw new GraphQLException(new Error($"{k.Message}", $"PRODUCT_NOT_FOUND"));
            }
            catch (Exception e)
            {
                throw new GraphQLException(new Error($"{e.Message}", $"PRODUCTY_DELETE_FAILED"));
            }
        }
    }
}
