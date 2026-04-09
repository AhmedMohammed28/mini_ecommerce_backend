using System;
using System.Threading.Tasks;
using mini_ecommerce_backend.Entities.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace mini_ecommerce_backend.Data;

public class mini_ecommerce_backendDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Product, Guid> _productRepository;

    public mini_ecommerce_backendDataSeederContributor(
        IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _productRepository.GetCountAsync() <= 0)
        {
            await _productRepository.InsertAsync(
                new Product(Guid.NewGuid(), "Laptop", 1500m, 10),
                autoSave: true
            );

            await _productRepository.InsertAsync(
                new Product(Guid.NewGuid(), "Mouse", 25m, 100),
                autoSave: true
            );
        }
    }
}