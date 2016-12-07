using System.Collections.Generic;

namespace MyBook.Models
{
    public class RoleViewModel
    {
        public RoleGridViewModel GridViewModel { get; set; }
    }

    public class RoleGridViewModel : GridViewModelBase
    {
        public List<RoleGridItem> GridItems { get; set; }

    }

    public class RoleGridItem
    {
        public int? ID { get; set; }
        public string Caption { get; set; }
        public int? Code { get; set; }
    }
}