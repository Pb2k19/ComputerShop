using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.User
{
    public class Login
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }
    }
}
