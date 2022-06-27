using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models
{
    public class Psu : IPsu
    {
        public string Manufacturer { get; set; } = string.Empty;
        public int Power { get; set; }
    }
    public static class PsuCertificates
    {
        public static string None { get; } = "Brak";
        public static string Bronze { get; } = "80 Plus Bronze";
        public static string Silver { get; } = "80 Plus Silver";
        public static string Gold { get; } = "80 Plus Gold";
        public static string Platinum { get; } = "80 Plus Platinum";
        public static string Titanium { get; } = "80 Plus Titanium";
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
