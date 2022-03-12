namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IComputerCase
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Lenghtmm { get; set; }
        public int Heightmm { get; set; }
        public int Widthmm { get; set; }
        public List<string> SupportedMoboSizes{ get; set; }
        public int UsbPorts { get; set; }
        public int MaxFanCount { get; set; }
        public int MaxCoolerHeightmm { get; set; }
        public int MaxRadiatorSizemm { get; set; }
        public int MaxGpuLenghtmm { get; set; }
        public string Color { get; set; }
    }
}
