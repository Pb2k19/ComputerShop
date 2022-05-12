using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.User
{
    public class DeliveryDetails
    {
        [Required(ErrorMessage = "Imię i nazwisko lub nazwa firmy jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Niepoprawne dane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adres email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail")]
        [StringLength(maximumLength: 128, ErrorMessage = "Adres email musi mieć mniej niż 128 znaków")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Niepoprawny kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Nazwa miejscowości musi mieć od 2 do 128 znaków")]
        public string City { get; set; }

        [Required(ErrorMessage = "Nazwa ulicy jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Nazwa ulicy musi mieć od 2 do 128 znaków")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Numer domu jest wymagany")]
        [StringLength(maximumLength: 64, MinimumLength = 1, ErrorMessage = "Numer domu musi mieć od 1 do 64 znaków")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Nieprawidłowy numer teleofnu")]
        [StringLength(maximumLength: 15, MinimumLength = 9, ErrorMessage = "Numer telefonu musi mieć od 9 do 15 znaków")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Metoda dostawy jest wymagana")]
        public string DeliveryMethod { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Sposób zapłaty jest wymagany")]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
