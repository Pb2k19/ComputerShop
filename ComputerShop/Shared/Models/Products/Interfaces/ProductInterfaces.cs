using ComputerShop.Shared.Models.ComputerComponents;
using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Products.Interfaces
{
    public interface IDesktopPcProduct : IProduct, IComputer
    {
        public int PowerConsumption { get; set; }
        public DesktopCase DesktopCase { get; set; }
        public Cooler Cooler { get; set; }
    }
    public interface IDesktopPsuProduct : IProduct, IDesktopPsu
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface ILaptopProduct : IProduct, IComputer, IDimensions
    {
        public decimal DisplaySize { get; set; }
        public int BatteryCapacitymAh { get; set; }
        public int BatteryLifeIdleMin { get; set; }
        public int BatteryLifeUnderLoadMin { get; set; }
    }
    public interface IDesktopGpuProduct : IProduct, IDesktopGpu
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface IDesktopMotherboardProduct : IProduct, IDesktopMotherboard
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface IRamProduct : IProduct, IRam
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface ICpuProduct : IProduct, ICpu
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
        public string SupportedSocket { get; set; }
        public List<string> SupportedChipsets { get; set; }
    }
    public interface ISsdProduct : IProduct, ISsd
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface IHddProduct : IHdd, IProduct
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface IComputerCaseProduct : IProduct, IComputerCase
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface IDesktopCoolerProduct : IProduct, ICooler
    {
        public new string Name { get; set; }
        public new string Manufacturer { get; set; }
    }
    public interface IComputerMouseProduct : IProduct, IDimensions
    {
        public int MaxDpi { get; set; }
        public int Weightg { get; set; }
        public bool IsWireless { get; set; }
        public string Sensor { get; set; }
        public string Interface { get; set; }
        public string SensorType { get; set; }
        public string Color { get; set; }
        public int PollingRateHz { get; set; }
    }
    public interface IKeyboardProduct : IProduct, IDimensions
    {
        public string KeyboardType { get; set; }
        public bool IsWireless { get; set; }
        public string Interface { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Weightg { get; set; }
    }
    public interface IHeadphonesProduct : IProduct, IDimensions
    {
        public bool IsWireless { get; set; }
        public string HeadphonesType { get; set; }
        public int ImpedanceOhm { get; set; }
        public int Weightg { get; set; }
        public int MinFrequencyResponseHz { get; set; }
        public int MaxFrequencyResponsekHz { get; set; }
        public string Interface { get; set; }
    }
    public interface IMonitorProduct:IProduct, IDimensions
    {
        public int PanelSizeInch { get; set; }
        public string AspectRatio { get; set; }
        public string PanelType { get; set; }
        public int ResolutionXpx { get; set; }
        public int ResolutionYpx { get; set; }
        public decimal SrgbColorSpacePerc { get; set; }
        public int BrightnessCdm { get; set; }
        public int Contrast { get; set; }
        public int RefreshRateHz { get; set; }
        public int ResponseTimems { get; set; }
        public List<string> Ports { get; set; }
    }
    public interface ICableProduct : IProduct
    {
        public int Lenghtmm { get; set; }
        public string CabelType { get; set; }
        public string ConnectorA { get; set; }
        public string ConnectorB { get; set; }
        public string Color { get; set; }
    }
}