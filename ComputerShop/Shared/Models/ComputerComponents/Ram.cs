using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models
{
    public class Ram : IRam
    {
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int ModulesNumber { get; set; }
        public int FrequencyMHz { get; set; }
        public int LatencyCL { get; set; }
        public string RamTechnology { get; set; } = string.Empty;
    }
    public enum RamTechnologies
    {
        DDR,
        DDR2,
        DDR3,
        DDR4,
        DDR5,
    }
}