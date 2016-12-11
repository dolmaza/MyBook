using Core.Utilities;
using SmartExpress.Reusable.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBook.Models
{
    public class OrderViewModel
    {
        public OrderGridViewModel GridViewModel { get; set; }

        public string AddNewOrderUrl { get; set; }
        public string OrdersUrl { get; set; }

    }

    public class OrderGridViewModel : GridViewModelBase
    {
        public List<OrderGridItem> GridItems { get; set; }
        public List<SimpleKeyValue<int?, string>> Users { get; set; }
        public List<SimpleKeyValue<int?, string>> Statuses { get; set; }

    }

    public class OrderGridItem
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

        public string DetailsUrl { get; set; }
        public string EdutUrl { get; set; }

    }

    public class OrdersFormViewModel
    {
        public int? ID { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string DeliveryTime { get; set; }
        public string Note { get; set; }

        public string OrdersUrl { get; set; }

        public OrderDetailsGridViewModel OrderDetailsGridViewModel { get; set; }

    }

    public class OrderDetailsGridViewModel : GridViewModelBase
    {
        public List<OrderDetailGridItem> GridItems { get; set; }
        public string TotalPrice => GridItems.Sum(t => t.Price?.ToDecimal()).ToString();
    }

    public class OrderDetailGridItem
    {
        public int? ID { get; set; }
        public string BookName { get; set; }
        public string Price { get; set; }
    }
}