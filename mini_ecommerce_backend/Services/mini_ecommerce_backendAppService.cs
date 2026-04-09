using Volo.Abp.Application.Services;
using mini_ecommerce_backend.Localization;

namespace mini_ecommerce_backend.Services;

/* Inherit your application services from this class. */
public abstract class mini_ecommerce_backendAppService : ApplicationService
{
    protected mini_ecommerce_backendAppService()
    {
        LocalizationResource = typeof(mini_ecommerce_backendResource);
    }
}