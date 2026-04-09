using System;
using System.ComponentModel.DataAnnotations;

namespace mini_ecommerce_backend.Services.Dtos.Orders
{
    public class CreateOrderItemDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
