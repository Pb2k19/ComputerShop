using ComputerShop.Shared.Models.Components;
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
    public interface ILaptopProduct : IProduct, IComputer
    {
        public decimal DisplaySize { get; set; }
        public int BatteryCapacitymAh { get; set; }
        public int BatteryLifeIdleMin { get; set; }
        public int BatteryLifeUnderLoadMin { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
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
}