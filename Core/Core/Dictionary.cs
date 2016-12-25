using System;
using System.Collections.Generic;

namespace Core
{
    public class Dictionary
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string CaptionEng { get; set; }
        public string StringCode { get; set; }
        public int? IntCode { get; set; }
        public decimal? DecimalValue { get; set; }
        public int? Level { get; set; }
        public string Hierarchy { get; set; }
        public int? Code { get; set; }
        public int? SortIndex { get; set; }
        public DateTime? CreateTime { get; set; }

        public Dictionary Parent { get; set; }

        public ICollection<Dictionary> Childrens { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Client> Clients { get; set; }

        public Dictionary()
        {
            Childrens = new List<Dictionary>();
            Orders = new List<Order>();
            Clients = new List<Client>();
            CreateTime = DateTime.Now;
        }
    }
}
