using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Components
{
    public class Cooler : ICooler
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<string> CompatibleSockets { get; set; }
        public string Type { get; set; }
    }

    public static class CoolerTypes
    {
        static string WaterCooler { get; } = "Chłodzenie wodne";
        static string AirCooler { get; } = "Chłodzenie powietrzne";
        static string BoxCooler { get; } = "Chłodzenie BOX";
        static string Passive { get; } = "Chłodzenie Pasywne";
    }

}
