using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class Cooler : ICooler
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<StringValue> CompatibleSockets { get; set; } = new();
        public int Sizemm { get; set; }
        public string CoolerType {get;set;}
    }

    public static class CoolerTypes
    {
        public static string WaterCooler { get; } = "Chłodzenie wodne";
        public static string AirCooler { get; } = "Chłodzenie powietrzne";
        public static string BoxCooler { get; } = "Chłodzenie BOX";
        public static string Passive { get; } = "Chłodzenie pasywne";
        public static List<string> List => new() { WaterCooler, AirCooler, BoxCooler, Passive };
    }
}
