using System.ComponentModel.DataAnnotations;

namespace jorge_api.Model
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
