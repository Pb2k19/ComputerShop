using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.User
{
    public class InvoiceDetails
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Niepoprawne dane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwa ulicy jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 3, ErrorMessage = "Nazwa ulicy musi mieć od 3 do 128 znaków")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Nazwa miejscowości musi mieć od 2 do 128 znaków")]
        public string City { get; set; }
    }
    public class InvoiceDetailsForBusiness : InvoiceDetails
    {
        [Required(ErrorMessage = "Numer NIP jest wymagany")]
        [RegularExpression(@"^((\d{3}[- ]\d{3}[- ]\d{2}[- ]\d{2})|(\d{3}[- ]\d{2}[- ]\d{2}[- ]\d{3}))$", ErrorMessage = "Niepoprawny numer NIP")]
        public string Nip { get; set; }
    }
}
