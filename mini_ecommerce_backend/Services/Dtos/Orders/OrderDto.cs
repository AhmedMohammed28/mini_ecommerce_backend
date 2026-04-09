using System;
using System.Collections.Generic;

namespace mini_ecommerce_backend.Services.Dtos.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public int TotalItemsCount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
    }
}
