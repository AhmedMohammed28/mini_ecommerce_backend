using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace mini_ecommerce_backend.Data;

public class mini_ecommerce_backendDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public mini_ecommerce_backendDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the mini_ecommerce_backendDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<mini_ecommerce_backendDbContext>()
            .Database
            .MigrateAsync();

    }
}
