using System.ComponentModel.DataAnnotations.Schema;

namespace jorge_api.Model
{
    public class Sales : BaseModel
    {
        public decimal Total { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
    }
}
