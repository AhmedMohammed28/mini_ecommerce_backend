using System.ComponentModel.DataAnnotations;

namespace mini_ecommerce_backend.Services.Dtos.Products
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
