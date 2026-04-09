using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace mini_ecommerce_backend.Entities.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }

        public ICollection<OrderItem> Items { get; private set; }

        private Order()
        {
            Items = new List<OrderItem>();
        }

        public Order(Guid id, string customerName, string customerEmail) : base(id)
        {
            SetCustomerName(customerName);
            SetCustomerEmail(customerEmail);
            Items = new List<OrderItem>();
        }

        public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero");
            }

            if (unitPrice <= 0)
            {
                throw new ArgumentException("Unit price must be greater than zero");
            }

            var existing = Items.FirstOrDefault(x => x.ProductId == productId);
            if (existing != null)
            {
                existing.IncreaseQuantity(quantity);
                return;
            }

            Items.Add(new OrderItem(Guid.NewGuid(), Id, productId, productName, unitPrice, quantity));
        }

        private void SetCustomerName(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("Customer name cannot be empty");
            }

            CustomerName = customerName;
        }

        private void SetCustomerEmail(string customerEmail)
        {
            if (string.IsNullOrWhiteSpace(customerEmail))
            {
                throw new ArgumentException("Customer email cannot be empty");
            }

            CustomerEmail = customerEmail;
        }
    }
}
