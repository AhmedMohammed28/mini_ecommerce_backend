using System.Linq.Dynamic.Core;
using mini_ecommerce_backend.Entities.Products;
using mini_ecommerce_backend.Services.Dtos.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace mini_ecommerce_backend.Services.Products
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductAppService(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = new Product(
                GuidGenerator.Create(),
                input.Name,
                input.Price,
                input.Quantity
            );

            await _productRepository.InsertAsync(product, autoSave: true);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _productRepository.GetQueryableAsync();
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? nameof(Product.Name) : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var items = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<ProductDto>(
                totalCount,
                items.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            );
        }

        public async Task<ListResultDto<ProductLookupDto>> GetLookupAsync()
        {
            var queryable = await _productRepository.GetQueryableAsync();
            var items = await AsyncExecuter.ToListAsync(queryable.OrderBy(x => x.Name));

            return new ListResultDto<ProductLookupDto>(
                items.Select(p => new ProductLookupDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    AvailableQuantity = p.Quantity
                }).ToList()
            );
        }
    }
}
