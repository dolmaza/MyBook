using System;
using System.Collections.Generic;

namespace Core
{
    public class Client
    {
        public int? ID { get; set; }
        public int? StatusID { get; set; }
        public int? UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public DateTime? CreateTime { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Dictionary Status { get; set; }
        public User User { get; set; }

        public Client()
        {
            CreateTime = DateTime.Now;
            Orders = new List<Order>();
        }
    }
}
