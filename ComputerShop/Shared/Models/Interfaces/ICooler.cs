namespace ComputerShop.Shared.Models.Interfaces
{
    public interface ICooler
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int FansCount { get; set; }
        public int MaxTdp { get; set; }
        public List<string> CompatibleSockets { get; set; }
        public string Type { get; set; }
    }
    public interface IWaterCooler : ICooler
    {
        public int RadiatorSizemm { get; set; }
    }
    public interface IAirCooler : ICooler
    {
        public int Height { get; set; }
    }
}
