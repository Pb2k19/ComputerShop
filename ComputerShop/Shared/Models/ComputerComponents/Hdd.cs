using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class Hdd : Drive, IHdd
    {
        public int Rpm { get; set; }
    }
}
