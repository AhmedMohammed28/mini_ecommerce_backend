using mini_ecommerce_backend.Localization;
using Volo.Abp.AspNetCore.Components;

namespace mini_ecommerce_backend;

public abstract class mini_ecommerce_backendComponentBase : AbpComponentBase
{
    protected mini_ecommerce_backendComponentBase()
    {
        LocalizationResource = typeof(mini_ecommerce_backendResource);
    }
}
