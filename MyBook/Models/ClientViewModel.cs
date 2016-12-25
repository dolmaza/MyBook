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
    }
}