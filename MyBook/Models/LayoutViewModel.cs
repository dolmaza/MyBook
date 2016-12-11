using Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace MyBook.Models
{
    public class LayoutViewModel
    {
        public string LogoutUrl { get; set; }
        public List<FirstLevelMenuItem> FirstLevelMenuItems { get; set; }
        public SuccessErrorMessageInfo SuccessErrorMessageInfo { get; set; }

        public LayoutViewModel()
        {
            FirstLevelMenuItems = new List<FirstLevelMenuItem>();
        }
    }

    public class FirstLevelMenuItem
    {
        public string Caption { get; set; }
        public string Url { get; set; }
        public string IconName { get; set; }
        public bool IsActive { get; set; }
        public bool IsMenuItemExpanded => SecondLevelMenuItems.Any(s => s.IsActive);
        public bool HasSecondLevelMenuItems => SecondLevelMenuItems.Count == 0;

        public List<SecondLevelMenuItem> SecondLevelMenuItems { get; set; }

        public class SecondLevelMenuItem
        {
            public string Caption { get; set; }
            public string Url { get; set; }
            public string IconName { get; set; }
            public bool IsActive { get; set; }

        }
    }
}