using System.Collections.Generic;

namespace MyBook.Models
{
    public class RolePermissionsViewModel
    {
        public RolePermissionsRolesGridViewModel RolesGridViewModel { get; set; }
        public RolePermissionsPermissionsTreeViewModel PermissionsTreeViewModel { get; set; }

        public string GetRolePermissionsUrl { get; set; }
        public string UpdateRolePermissionsUrl { get; set; }

    }

    public class RolePermissionsRolesGridViewModel : GridViewModelBase
    {
        public List<RolePermissionsRoleGridItem> GridItems { get; set; }

    }

    public class RolePermissionsRoleGridItem
    {
        public int? ID { get; set; }
        public string Caption { get; set; }

    }

    public class RolePermissionsPermissionsTreeViewModel : TreeListVeiwModelBase
    {
        public List<RolePermissionsPermissionTreeItem> TreeItems { get; set; }
    }

    public class RolePermissionsPermissionTreeItem
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
    }
}