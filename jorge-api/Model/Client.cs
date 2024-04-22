using System.ComponentModel.DataAnnotations;

namespace jorge_api.Model
{
    public class Client: BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
