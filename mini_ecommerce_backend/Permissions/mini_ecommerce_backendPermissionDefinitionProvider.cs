using mini_ecommerce_backend.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace mini_ecommerce_backend.Permissions;

public class mini_ecommerce_backendPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(mini_ecommerce_backendPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(mini_ecommerce_backendPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<mini_ecommerce_backendResource>(name);
    }
}
