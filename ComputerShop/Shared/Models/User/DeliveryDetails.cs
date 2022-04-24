using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.User
{
    public class DeliveryDetails
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail")]
        [StringLength(maximumLength: 128, ErrorMessage = "Adres email musi mieć mniej niż 128 znaków")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"[0-9]{2}-[0-9]{3}", ErrorMessage = "Niepoprawny kod pocztowy")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        [StringLength(maximumLength: 128, ErrorMessage = "Nazwa miejscowości musi mieć mniej niż 128 znaków")]
        public string City { get; set; }

        [Required(ErrorMessage = "Nazwa ulicy jest wymagana")]
        [StringLength(maximumLength: 128, ErrorMessage = "Nazwa ulicy musi mieć mniej niż 128 znaków")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Numer domu jest wymagany")]
        [StringLength(maximumLength: 128, ErrorMessage = "Numer domu musi mieć mniej niż 128 znaków")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Nieprawidłowy numer teleofnu")]
        public string PhoneNumber { get; set; }

        public bool QuickValidate()
        {
            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(City) ||
               string.IsNullOrWhiteSpace(HouseNumber) || string.IsNullOrWhiteSpace(PhoneNumber) ||
               string.IsNullOrWhiteSpace(PostCode) || string.IsNullOrWhiteSpace(Street))
                return false;
            return true;
        }
    }
}
