using mini_ecommerce_backend.Services.Dtos.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace mini_ecommerce_backend.Services.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> CreateAsync(CreateProductDto input);
        Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    }
}
