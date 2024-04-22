using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace jorge_api.Model
{
    public class Products : BaseModel
    {
        public string? Description { get; set; } = null;
        [Required]
        public decimal Price { get; set; }
    }
}
