using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class DesktopPsuProduct : Product, IDesktopPsuProduct
    {
        public DesktopPsuProduct()
        {
            Category = "PSU";
        }
        public int Pcie8pinCount { get; set; }
        public int MolexCount { get; set; }
        public int Pcie6pinCount { get; set; }
        public bool IsModular { get; set; }
        public int FansCount { get; set; }
        public string Certificate { get; set; }
        public int Power { get; set; }
        public int SataCount { get; set; }
        public List<string> Protections { get; set; } = new();
    }
    public static class PsuProtections
    {
        public static string OCP { get; } = "OCP - Over-Current Protection";
        public static string OVP { get; } = "OVP - Over Voltage Protection";
        public static string UVP { get; } = "UVP - Under Voltage Protection";
        public static string OPP { get; } = "OPP - Over Power Protection";
        public static string OTP { get; } = "OTP - Over Temperature Protection";
        public static string SCP { get; } = "SCP - Short Circuit Protection";
        public static List<string> List { get; } = new()
        {
            OCP,
            OVP,
            UVP,
            OPP,
            OTP,
            SCP,
        };
    }
}
