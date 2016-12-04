using System;
using System.Collections.Generic;

namespace Core
{
    public class Role
    {
        public int? ID { get; set; }
        public string Caption { get; set; }
        public int? Code { get; set; }
        public DateTime? CreateTime { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Permission> Permissions { get; set; }

        public Role()
        {
            Users = new List<User>();
            Permissions = new List<Permission>();
            CreateTime = DateTime.Now;
        }
    }
}
