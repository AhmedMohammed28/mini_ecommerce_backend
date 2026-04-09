using mini_ecommerce_backend.Menus;
using mini_ecommerce_backend.Localization;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.UI.Navigation;

namespace mini_ecommerce_backend.Menus;

public class mini_ecommerce_backendMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<mini_ecommerce_backendResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                mini_ecommerce_backendMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 4;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->Tenant Management
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 2);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 8);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Products",
                "Products",
                url: "/products",
                icon: "fa fa-box",
                order: 2
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Orders.New",
                "Create Order",
                url: "/orders/new",
                icon: "fa fa-shopping-cart",
                order: 3
            )
        );

        return Task.CompletedTask;
    }
}
