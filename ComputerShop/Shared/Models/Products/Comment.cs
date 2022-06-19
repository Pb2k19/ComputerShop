using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Shared.Models.Products
{
    public class Comment
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Niepoprawne dane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Komentarz jest wymagany")]
        [StringLength(maximumLength: 1024, MinimumLength = 2, ErrorMessage = "Maksymalna długość komentarza to 1024 znaków")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Ocena jest wymagana")]
        [Range(1, 5, ErrorMessage = "Ocena powinna być z zakresu od 1 do 5")]
        public int Score { get; set; } = 5;
    }
}
