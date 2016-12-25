namespace Core.Utilities
{
    public class SimpleKeyValue<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
