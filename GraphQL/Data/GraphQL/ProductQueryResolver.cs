﻿using GraphQL.Data.Entities;
using GraphQL.Services.Core.IServices;

namespace GraphQL.Data.GraphQL
{
    [ExtendObjectType("Query")]
    public class ProductQueryResolver
    {
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts([Service] IProductService productService)
        {
            return productService.GetAll();
        }
    }
}
