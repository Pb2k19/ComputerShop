namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IGpu
    {
        public string ChipManufacturer { get; set; }
        public string Name { get; set; }
        public string VramType { get; set; }
        public int VramSizeGB { get; set; }
        public int FrequencyMHz { get; set; }
        public int MemoryFrequencyMHz { get; set; }
        public int Tdp { get; set; }
    }

    public interface IDesktopGpu : IGpu
    {
        public string Manufacturer { get; set; }
        public int Lenghtmm { get; set; }
    }
}
