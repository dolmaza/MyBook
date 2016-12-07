using System;
using System.Collections.Generic;

namespace Core
{
    public class Permission
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public string IconName { get; set; }
        public bool IsMenuItem { get; set; }
        public string Code { get; set; }
        public int? SortIndex { get; set; }
        public DateTime? CreateTime { get; set; }

        public Permission Parent { get; set; }

        public ICollection<Permission> Childrens { get; set; }

        public ICollection<Role> Roles { get; set; }

        public Permission()
        {
            Roles = new List<Role>();
            Childrens = new List<Permission>();
            CreateTime = DateTime.Now;
        }
    }
}
