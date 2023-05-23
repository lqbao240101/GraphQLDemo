using GraphQL.Services.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Entities
{
    [Index(nameof(ProductName), IsUnique = true)]
    public class Product : EntityBase
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
