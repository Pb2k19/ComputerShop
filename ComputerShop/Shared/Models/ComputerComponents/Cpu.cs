using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models
{
    public class Cpu : ICpu
    {
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public int ThreadsCount { get; set; }
        public int CoresCount { get; set; }
        public int L3CacheMB { get; set; }
        public string Architecture { get; set; } = string.Empty;
    }
}
