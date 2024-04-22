using System.ComponentModel.DataAnnotations.Schema;

namespace jorge_api.Model
{
    public class SalesDetails : BaseModel
    {
        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public Sales Sale { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProoductId")]
        public Products Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
