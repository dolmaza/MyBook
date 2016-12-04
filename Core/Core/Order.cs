using System;
using System.Collections.Generic;

namespace Core
{
    public class Order
    {
        public int? ID { get; set; }
        public int? UserID { get; set; }
        public int? StatusID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string Note { get; set; }
        public DateTime? CreateTime { get; set; }

        public User User { get; set; }
        public Dictionary Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            CreateTime = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }
    }
}
