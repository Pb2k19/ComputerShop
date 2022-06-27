using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class CpuProduct : Product, ICpuProduct
    {
        public CpuProduct()
        {
            Category = "CPU";
        }
        public int FrequencyMHz { get; set; }
        public int Tdp { get; set; }
        public int ThreadsCount { get; set; }
        public int CoresCount { get; set; }
        public int L3CacheMB { get; set; }
        public string SupportedSocket { get; set; } = string.Empty;
        public List<StringValue> SupportedChipsets { get; set; } = new();
    }
}
