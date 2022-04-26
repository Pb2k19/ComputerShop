using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.User
{
    public class Login
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        [EmailAddress(ErrorMessage = "Adres email jest nieprawidłowy")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
