namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IPsu
    {
        public string Manufacturer { get; set; }
        public int Power { get; set; }
    }
    public interface IDesktopPsu : IPsu
    {
        public string Name { get; set; }
        public int Pcie8pinCount { get; set; }
        public int MolexCount { get; set; }
        public int Pcie6pinCount { get; set; }
        public bool IsModular { get; set; }
        public int FansCount { get; set; }
        public string Certificate { get; set; }
    }
}
