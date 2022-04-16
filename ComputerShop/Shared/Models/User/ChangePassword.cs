using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.User
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Aktualne hasło jest wymagane")]
        [StringLength(maximumLength: 30, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(maximumLength: 30, MinimumLength = 8, ErrorMessage = "Hasło musi mieć od 8 do 30 znaków")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{1,}$",
            ErrorMessage = "Hasło musi posiadać przynajmniej małą i dużą literę, cyfrę i znak specjalny (!@#$&*)")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła nie są identyczne")]
        [DataType(DataType.Password)]
        public string ConfPassword { get; set; }
    }
}
