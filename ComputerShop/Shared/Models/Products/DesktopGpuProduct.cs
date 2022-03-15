using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopGpuProduct : Product, IDesktopGpuProduct
    {
        public DesktopGpuProduct()
        {
            Category = new Category()
            {
                Id = "8998", //tmp
                Name = "GPU",
                Url = "gpu"
            };
        }
        public string ChipManufacturer { get; set; }
        public int FrequencyMHz { get; set; }
        public int MemoryFrequencyMHz { get; set; }
        public string VramType { get; set; }
        public int VramSizeGB { get; set; }
        public int Tdp { get; set; }
        public int Lenghtmm { get; set; }
        public List<string> PortsList { get; set; }
        public int BusWidth { get; set; }
        public List<string> PowerConnectors { get; set; }
    }
}
