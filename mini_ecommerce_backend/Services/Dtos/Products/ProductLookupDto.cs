namespace mini_ecommerce_backend.Services.Dtos.Products
{
    public class ProductLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
