using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class Cooler : ICooler
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<string> CompatibleSockets { get; set; }
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
