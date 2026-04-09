using System;
using System.Threading.Tasks;
using mini_ecommerce_backend.Entities.Books;
using mini_ecommerce_backend.Entities.Products;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace mini_ecommerce_backend.Data;

public class mini_ecommerce_backendDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Book, Guid> _bookRepository;
    private readonly IRepository<Product, Guid> _productRepository;

    public mini_ecommerce_backendDataSeederContributor(
        IRepository<Book, Guid> bookRepository,
        IRepository<Product, Guid> productRepository)
    {
        _bookRepository = bookRepository;
        _productRepository = productRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _bookRepository.GetCountAsync() <= 0)
        {
            await _bookRepository.InsertAsync(
                new Book
                {
                    Name = "1984",
                    Type = BookType.Dystopia,
                    PublishDate = new DateTime(1949, 6, 8),
                    Price = 19.84f
                },
                autoSave: true
            );

            await _bookRepository.InsertAsync(
                new Book
                {
                    Name = "The Hitchhiker's Guide to the Galaxy",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(1995, 9, 27),
                    Price = 42.0f
                },
                autoSave: true
            );
        }

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