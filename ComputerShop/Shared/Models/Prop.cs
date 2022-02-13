namespace ComputerShop.Shared.Models
{
    public class Prop : IProp
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public interface IProp
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
