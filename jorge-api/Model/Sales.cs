using System.ComponentModel.DataAnnotations.Schema;

namespace jorge_api.Model
{
    public class Sales : BaseModel
    {
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public decimal Total { get; set; }
        public virtual SalesDetails[] Details { get; set; } = Array.Empty<SalesDetails>();
    }
}
