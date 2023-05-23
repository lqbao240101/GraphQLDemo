using AutoMapper;
using GraphQL.Data.Entities;
using GraphQL.Data.GraphQL;
using GraphQL.Data.Repositories.Category;
using GraphQL.Data.Repositories.Product;
using GraphQL.Services.Core.Catalog;
using GraphQL.Services.Core.Common;
using GraphQL.Services.Core.IServices;
using GraphQL.Services.Mapper;

namespace GraphQL.Application.Extensions
{
    public static class ServiceDI
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IBaseService<Category>, BaseService<Category>>();
            services.AddTransient<IBaseService<Product>, BaseService<Product>>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddType<CategoryQueryResolver>()
                .AddType<ProductQueryResolver>()
                .AddMutationType(m => m.Name("Mutation"))
                .AddType<ProductMutationResolver>()
                .AddType<CategoryMutationResolver>();
        }

        public static void AddMappingProfile(this IServiceCollection services)
        {
            var profiles = new MapperConfiguration(
                _ => { _.AddProfile(new AutoMapping());
                }
            );

            services.AddSingleton(profiles.CreateMapper());
        }
    }
}
