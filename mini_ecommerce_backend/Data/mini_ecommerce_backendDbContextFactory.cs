using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace mini_ecommerce_backend.Data;

public class mini_ecommerce_backendDbContextFactory : IDesignTimeDbContextFactory<mini_ecommerce_backendDbContext>
{
    public mini_ecommerce_backendDbContext CreateDbContext(string[] args)
    {
        mini_ecommerce_backendGlobalFeatureConfigurator.Configure();
        mini_ecommerce_backendModuleExtensionConfigurator.Configure();
        
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<mini_ecommerce_backendDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new mini_ecommerce_backendDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}