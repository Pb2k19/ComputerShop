using ComputerShop.Shared.Models.Components;

namespace ComputerShop.Shared.Models.Interfaces
{
    public interface IComputer
    {
        public Cpu Cpu { get; set; }
        public Gpu Gpu { get; set; }
        public Ram Ram { get; set; }
        public Psu Psu { get; set; }
        public List<Drive> Drives { get; set; }
    }
}
