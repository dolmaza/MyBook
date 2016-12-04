using System;

namespace Core
{
    public class OrderDetail
    {
        public int? ID { get; set; }
        public int? OrderID { get; set; }
        public string BookName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreateTime { get; set; }

        public Order Order { get; set; }

        public OrderDetail()
        {
            CreateTime = DateTime.Now;
        }
    }
}
