using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mini_ecommerce_backend.Services.Dtos.Orders
{
    public class CreateOrderDto
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
