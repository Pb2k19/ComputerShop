using ComputerShop.Shared.Models.Interfaces;

namespace ComputerShop.Shared.Models
{
    public class Psu : IPsu
    {
        public string Manufacturer { get; set; }
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
    public enum PsuManufacturers
    {
        Antec,
        Corsair,
        EVGA,
        FSP,
        NZXT,
        OCZ,
        Seasonic,
        SilverStone,
        XFX
    }
}
