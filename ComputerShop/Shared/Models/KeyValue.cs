namespace ComputerShop.Shared.Models
{
    public class KeyValue : IKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public interface IKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
