using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.ComputerComponents
{
    public class Ssd : Drive, ISsd
    {
        public int Tbw { get; set; }
    }
}
