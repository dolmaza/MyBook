using Core.Utilities;
using System.Collections.Generic;

namespace MyBook.Models
{
    public class ClientViewModel
    {
        public ClientGridViewModel GridViewModel { get; set; }

    }

    public class ClientGridViewModel : GridViewModelBase
    {
        public List<ClientGridItem> GridItems { get; set; }

        public List<SimpleKeyValue<int?, string>> Statuses { get; set; }

        public string CreateOrderUrl { get; set; }

    }

    public class ClientGridItem
    {
        public int? ID { get; set; }
        public int? StatusID { get; set; }
        public int? UserID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }

        public string ClientBooksUrl { get; set; }
    }

    public class ClientBooksViewModel
    {
        public ClientBooksGridViewModel GridViewModel { get; set; }

    }

    public class ClientBooksGridViewModel : GridViewModelBase
    {
        public List<ClientBookGridItem> GridItems { get; set; }

    }

    public class ClientBookGridItem
    {
        public int? ID { get; set; }
        public string BookName { get; set; }
        public string Price { get; set; }

    }
}