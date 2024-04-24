using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace jorge_api.Model
{
    public class Products : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
