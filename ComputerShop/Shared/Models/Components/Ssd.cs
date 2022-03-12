using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models.Components
{
    public class Ssd : Drive, ISsd
    {
        public int Tbw { get; set; }
    }
}
