using mini_ecommerce_backend.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace mini_ecommerce_backend.Permissions;

public class mini_ecommerce_backendPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(mini_ecommerce_backendPermissions.GroupName);


        var booksPermission = myGroup.AddPermission(mini_ecommerce_backendPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(mini_ecommerce_backendPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(mini_ecommerce_backendPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(mini_ecommerce_backendPermissions.Books.Delete, L("Permission:Books.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(mini_ecommerce_backendPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<mini_ecommerce_backendResource>(name);
    }
}
