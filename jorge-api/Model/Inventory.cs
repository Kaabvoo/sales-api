using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace jorge_api.Model
{
    [Keyless]
    public class Inventory
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        public int InStore {  get; set; }
    }
}
