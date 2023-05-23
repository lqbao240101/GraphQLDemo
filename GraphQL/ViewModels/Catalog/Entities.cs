using GraphQL.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQL.ViewModels.Catalog
{
    public class Entities
    {
        public class CategoryRequest
        {
            public string? CategoryName { get; set; } = default!;
            public string? Description { get; set; } = default!;
        }

        public class ProductRequest
        {
            public string? ProductName { get; set; } = default!;
            public string? Description { get; set; } = default!;
            public decimal? Price { get; set; }
            public int? Quantity { get; set; }
            public Guid? CategoryId { get; set; }
        }
    }
}
