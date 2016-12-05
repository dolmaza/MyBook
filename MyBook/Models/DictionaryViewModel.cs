using System.Collections.Generic;

namespace MyBook.Models
{
    public class DictionaryViewModel
    {
        public DictionaryTreeViewModel TreeViewModel { get; set; }

    }

    public class DictionaryTreeViewModel : TreeListVeiwModelBase
    {
        public List<DictionaryTreeItem> TreeItems { get; set; }
    }

    public class DictionaryTreeItem
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string CaptionEng { get; set; }
        public string StringCode { get; set; }
        public int? IntCode { get; set; }
        public decimal? DecimalValue { get; set; }
        public int? Code { get; set; }
        public int? SortIndex { get; set; }
    }
}