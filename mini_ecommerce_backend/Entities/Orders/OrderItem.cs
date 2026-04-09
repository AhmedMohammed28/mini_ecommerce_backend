using System;
using Volo.Abp.Domain.Entities;

namespace mini_ecommerce_backend.Entities.Orders
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem()
        {
        }

        public OrderItem(Guid id, Guid orderId, Guid productId, string productName, decimal unitPrice, int quantity)
            : base(id)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero");
            }

            Quantity += quantity;
        }

        public decimal GetTotal() => UnitPrice * Quantity;
    }
}
