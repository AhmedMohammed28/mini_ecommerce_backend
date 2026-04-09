using System;
using System.Threading.Tasks;
using mini_ecommerce_backend.Services.Dtos.Orders;
using Volo.Abp.Application.Services;

namespace mini_ecommerce_backend.Services.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto input);
        Task<OrderDto> GetAsync(Guid id);
    }
}
