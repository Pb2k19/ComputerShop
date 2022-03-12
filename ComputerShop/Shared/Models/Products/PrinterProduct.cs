using ComputerShop.Shared.Models.Products.Interfaces;

namespace ComputerShop.Shared.Models.Products
{
    public class PrinterProduct : Product, IPrinterProduct
    {
        public PrinterProduct()
        {
            Category = new Category { Name = "Printer", Url = "printer", Id="5231" };
        }
        public string Technology {get; set;}
        public List<string> SupportedFormats { get; set; }
        public int PrintSpeedPgpmin { get; set; }
        public List<string> Interfaces { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasScanner { get; set; }
        public bool HasMulticolour { get; set; }
        public string Color { get; set; }
        public int Widthmm { get; set; }
        public int Heightmm { get; set; }
        public int Lenghtmm { get; set; }
    }
}
