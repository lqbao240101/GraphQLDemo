using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GraphQL.Services.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Entities
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category : EntityBase
    {
        [Required]
        [MinLength(2, ErrorMessage = "Tên phải có ít nhất 2 ký tự")]
        public string CategoryName { get; set; } = default!;

        [Required]
        [MinLength(10, ErrorMessage = "Thông tin phải có ít nhất 10 ký tự")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; } = default!;

        public List<Product>? Products { get; set; }
    }
}
