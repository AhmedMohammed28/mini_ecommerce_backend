namespace mini_ecommerce_backend.Permissions;

public static class mini_ecommerce_backendPermissions
{
    public const string GroupName = "mini_ecommerce_backend";


    public static class Books
    {
        public const string Default = GroupName + ".Books";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
