using System;
using System.Collections.Generic;

namespace Core
{
    public class User
    {
        public int? ID { get; set; }
        public int? RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateTime { get; set; }

        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Client> Clients { get; set; }

        public User()
        {
            CreateTime = DateTime.Now;
            Orders = new List<Order>();
            Clients = new List<Client>();
        }
    }
}
