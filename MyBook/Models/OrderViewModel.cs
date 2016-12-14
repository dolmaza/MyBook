﻿using Core.Properties;
using Core.Utilities;
using SmartExpress.Reusable.Extentions;
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

        public bool ShowUserColumn { get; set; }
        public bool IsAllowedToChangeStatus { get; set; }

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
        public string TotalPrice { get; set; }
        public string DeliveryTime { get; set; }
        public string Note { get; set; }
        public string CreateTime { get; set; }

        public string DetailsUrl { get; set; }
        public string EdutUrl { get; set; }

    }

    public class OrdersFormViewModel
    {
        public int? ID { get; set; }
        public int? UserID { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string DeliveryTime { get; set; }
        public string Note { get; set; }

        public string FormatDateJs { get; set; } = Resources.FormatDateJs;
        public string SaveOrderPropertiesUrl { get; set; }
        public string OrdersUrl { get; set; }

        public OrderDetailsGridViewModel OrderDetailsGridViewModel { get; set; }

    }

    public class OrderDetailsGridViewModel : GridViewModelBase
    {
        public List<OrderDetailGridItem> GridItems { get; set; }
    }

    public class OrderDetailGridItem
    {
        public int? ID { get; set; }
        public string BookName { get; set; }
        public string Price { get; set; }
    }
}