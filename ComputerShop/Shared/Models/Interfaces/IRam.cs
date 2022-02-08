namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IRam
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int ModulesNumber { get; set; }
        public int FrequencyMHz { get; set; }
        public int LatencyCL { get; set; }
        public string RamTechnology { get; set; }
    }
}
