namespace jorge_api.DTO
{
    public class CreateSale
    {
        public int ClientId { get; set; }
        public ProductCount[] Products { get; set; } = [];
    }
    public class ProductCount
    {
        public int ProductId { get; set; }
        public int Count{ get; set; }
    }
}
