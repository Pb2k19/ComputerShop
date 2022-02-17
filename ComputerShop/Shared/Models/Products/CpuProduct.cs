using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class CpuProduct : Product, ICpuProduct
    {
        public CpuProduct()
        {
            Category = new Category { Id = "45645", Name = "CPU", Url = "cpu" };
        }
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public int ThreadsCount { get; set; }
        public int CoresCount { get; set; }
        public int L3CacheMB { get; set; }
        public string SupportedSocket { get; set; }
        public List<string> SupportedChipsets { get; set; }
    }
}
