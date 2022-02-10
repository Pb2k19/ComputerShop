using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Components
{
    public class DesktopCase : IComputerCase
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Lenghtmm { get; set; }
        public int Heightmm { get; set; }
        public int Widthmm { get; set; }
        public List<string> SupportedMoboSizes { get; set; }
        public int UsbPorts { get; set; }
        public int MaxFanCount { get; set; }
        public int MaxCoolerHeightmm { get; set; }
        public int MaxRadiatorSizemm { get; set; }
        public int MaxGpuLenght { get; set; }
    }

    public enum DesktopCaseManufacturers
    {
        Thermaltake,
        CoolerMaster,
        Aerocool,
        Chieftec,
        Phanteks,
        LianLi,
        BeQuiet,
        SilentiumPC,
        Nzxt,
        Corsair,
    }
}
