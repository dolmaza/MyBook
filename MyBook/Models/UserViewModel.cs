using Core.Utilities;
using System.Collections.Generic;

namespace MyBook.Models
{
    public class UserViewModel
    {
        public UserGridViewModel GridViewModel { get; set; }
    }

    public class UserGridViewModel : GridViewModelBase
    {
        public List<UserGridItem> GridItems { get; set; }
        public List<SimpleKeyValue<int?, string>> Roles { get; set; }

    }

    public class UserGridItem
    {
        public int? ID { get; set; }
        public int? RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
    }
}