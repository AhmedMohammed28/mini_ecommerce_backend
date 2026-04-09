using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using mini_ecommerce_backend.Entities.Products;
using mini_ecommerce_backend.Entities.Orders;

namespace mini_ecommerce_backend.Data;

public class mini_ecommerce_backendDbContext : AbpDbContext<mini_ecommerce_backendDbContext>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public const string DbTablePrefix = "App";
    public const string DbSchema = null;

    public mini_ecommerce_backendDbContext(DbContextOptions<mini_ecommerce_backendDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();

        /* Configure your own entities here */
        builder.Entity<Product>(b =>
        {
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Price).IsRequired();
            b.Property(x => x.Quantity).IsRequired();

            b.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Product_Price_Quantity_Valid",
                    "[Price] > 0 AND [Quantity] >= 0"
                );
            });
        });

        builder.Entity<Order>(b =>
        {
            b.ToTable(DbTablePrefix + "Orders", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.CustomerName).IsRequired().HasMaxLength(128);
            b.Property(x => x.CustomerEmail).IsRequired().HasMaxLength(256);

            b.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<OrderItem>(b =>
        {
            b.ToTable(DbTablePrefix + "OrderItems", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.ProductName).IsRequired().HasMaxLength(256);
            b.Property(x => x.UnitPrice).IsRequired();
            b.Property(x => x.Quantity).IsRequired();

            b.HasIndex(x => x.OrderId);
            b.HasIndex(x => x.ProductId);
        });
    }
}

