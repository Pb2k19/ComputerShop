using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Components
{
    public class Hdd : Drive, IHdd
    {
        public int Rpm { get; set; }
    }
}
