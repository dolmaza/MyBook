using Core.Properties;
using Core.Utilities;
using System.Collections.Generic;

namespace MyBook.Models
{
    public class OrderViewModel
    {
        public OrderGridViewModel GridViewModel { get; set; }

        public bool IsAllowedToArchiveOrders { get; set; }
        public string ArchiveOrdersUrl { get; set; }

        public string AddNewOrderUrl { get; set; }

    }

    public class OrderGridViewModel : GridViewModelBase
    {
        public List<OrderGridItem> GridItems { get; set; }
        public List<SimpleKeyValue<int?, string>> Users { get; set; }
        public List<SimpleKeyValue<int?, string>> Statuses { get; set; }

        public bool ShowUserColumn { get; set; }
        public bool IsAllowedToChangeStatus { get; set; }
        public bool IsAllowedToDeleteOrder { get; set; }

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

        public string PaperUrl { get; set; }
        public bool IsAllowedToEditOrder { get; set; }
        public string EdutUrl { get; set; }
        public string StatusUpdateUrl { get; set; }

    }

    public class OrderArchivedViewModel
    {
        public OrderArchivedGridViewModel GridViewModel { get; set; }

        public bool IsAllowedToUnArchiveOrders { get; set; }
        public string UnArchiveOrdersUrl { get; set; }


    }

    public class OrderArchivedGridViewModel : GridViewModelBase
    {
        public List<OrderArchivedGridItem> GridItems { get; set; }
        public List<SimpleKeyValue<int?, string>> Users { get; set; }

        public bool ShowUserColumn { get; set; }
        public bool IsAllowedToDeleteOrder { get; set; }

    }

    public class OrderArchivedGridItem
    {
        public int? ID { get; set; }
        public int? UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string TotalPrice { get; set; }
        public string DeliveryTime { get; set; }
        public string Note { get; set; }
        public string CreateTime { get; set; }

        public string PaperUrl { get; set; }

    }

    public class OrdersFormViewModel
    {
        public int? ID { get; set; }
        public int? UserID { get; set; }
        public int? StatusID { get; set; }
        public int? ClientID { get; set; }
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

    public class OrderStatusUpdateViewModel
    {
        public List<SimpleKeyValue<int?, string>> Statuses { get; set; }
        public int? StatusID { get; set; }

        public string StatusSaveUrl { get; set; }

    }

    public class OrderPaperViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string DeliveryTime { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        public GridViewModel Grid { get; set; }

        public class GridViewModel : GridViewModelBase
        {
            public List<GridItem> GridItems { get; set; }

            public class GridItem
            {
                public int? ID { get; set; }
                public string BookName { get; set; }
                public string Price { get; set; }

            }
        }
    }

    public class SoldBooksViewModel
    {
        public SoldBooksGridViewModel GridViewModel { get; set; }

    }

    public class SoldBooksGridViewModel : GridViewModelBase
    {
        public List<SoldBookGridItem> GridItems { get; set; }
        public List<SimpleKeyValue<int?, string>> Clients { get; set; }

        public string CreateOrderUrl { get; set; }

    }

    public class SoldBookGridItem
    {
        public int? ID { get; set; }
        public string BookName { get; set; }
        public string Price { get; set; }

        public int? ClientID { get; set; }
        public string Mobile { get; set; }

    }
}