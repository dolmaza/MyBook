using System.Collections.Generic;

namespace MyBook.Models
{
    public class PermissionViewModel
    {
        public PermissionTreeViewModel TreeViewModel { get; set; }

    }

    public class PermissionTreeViewModel : TreeListVeiwModelBase
    {
        public List<PermissionTreeItem> TreeItems { get; set; }

    }

    public class PermissionTreeItem
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public string IconName { get; set; }
        public bool IsMenuItem { get; set; }
        public string Code { get; set; }
        public int? SortIndex { get; set; }
    }
}