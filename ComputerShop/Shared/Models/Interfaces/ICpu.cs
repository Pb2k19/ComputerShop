namespace ComputerShop.Shared.Models.Interfaces
{
    public interface ICpu
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public int ThreadsCount { get; set; }
        public int CoresCount { get; set; }
        public int L3CacheMB { get; set; }
    }
}
