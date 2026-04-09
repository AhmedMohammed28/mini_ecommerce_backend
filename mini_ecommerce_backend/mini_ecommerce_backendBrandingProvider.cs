using Microsoft.Extensions.Localization;
using mini_ecommerce_backend.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace mini_ecommerce_backend;

[Dependency(ReplaceServices = true)]
public class mini_ecommerce_backendBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<mini_ecommerce_backendResource> _localizer;

    public mini_ecommerce_backendBrandingProvider(IStringLocalizer<mini_ecommerce_backendResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
