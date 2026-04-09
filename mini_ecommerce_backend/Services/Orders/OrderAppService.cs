using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mini_ecommerce_backend.Entities.Orders;
using mini_ecommerce_backend.Entities.Products;
using mini_ecommerce_backend.Services.Dtos.Orders;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace mini_ecommerce_backend.Services.Orders
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public OrderAppService(
            IRepository<Order, Guid> orderRepository,
            IRepository<Product, Guid> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [UnitOfWork]
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var productIds = input.Items.Select(x => x.ProductId).Distinct().ToList();
            var products = await _productRepository.GetListAsync(x => productIds.Contains(x.Id));

            if (products.Count != productIds.Count)
            {
                throw new UserFriendlyException("One or more products not found");
            }

            foreach (var item in input.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                if (product.Quantity < item.Quantity)
                {
                    throw new UserFriendlyException($"Insufficient stock for product {product.Name}");
                }
            }

            foreach (var item in input.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                product.DecreaseQuantity(item.Quantity);
                await _productRepository.UpdateAsync(product, autoSave: true);
            }

            var order = new Order(GuidGenerator.Create(), input.CustomerName, input.CustomerEmail);
            foreach (var item in input.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                order.AddItem(product.Id, product.Name, product.Price, item.Quantity);
            }

            await _orderRepository.InsertAsync(order, autoSave: true);

            return await GetAsync(order.Id);
        }

        public async Task<OrderDto> GetAsync(Guid id)
        {
            var queryable = await _orderRepository.WithDetailsAsync(x => x.Items);
            var order = await queryable.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order), id);
            }

            var items = order.Items
                .Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    Total = i.GetTotal()
                })
                .ToList();

            var totalItemsCount = items.Sum(x => x.Quantity);
            var subtotal = items.Sum(x => x.Total);

            var discountRate = totalItemsCount >= 5 ? 0.10m : (totalItemsCount >= 2 ? 0.05m : 0m);
            var discountAmount = Math.Round(subtotal * discountRate, 2, MidpointRounding.AwayFromZero);
            var total = subtotal - discountAmount;

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                Items = items,
                TotalItemsCount = totalItemsCount,
                Subtotal = subtotal,
                DiscountRate = discountRate,
                DiscountAmount = discountAmount,
                Total = total
            };
        }
    }
}
