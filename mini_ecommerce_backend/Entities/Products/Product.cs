using System;
using Volo.Abp.Domain.Entities.Auditing;


namespace mini_ecommerce_backend.Entities.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        private Product()
        {
        }
        public Product(Guid id, string name, decimal price, int quantity) : base(id)
        {
            SetName(name);
            SetPrice(price);
            SetQuantity(quantity);
        }
        private void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative");

            Quantity = quantity;
        }
        private void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            Price = price;
        }
        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty");

            Name = name;

        }
    }
}
