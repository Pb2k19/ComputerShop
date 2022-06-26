using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Shared.Models.Interfaces
{
    public interface ICooler
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<StringValue> CompatibleSockets { get; set; }
        public string CoolerType { get; set; }
        public int Sizemm { get; set; }
    }
}