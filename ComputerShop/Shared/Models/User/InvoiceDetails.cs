using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComputerShop.Shared.Models.User
{
    public class InvoiceDetails : IValidatableObject
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        public bool IsBusiness { get; set; } = true;

        [Required(ErrorMessage = "Pole jest wymagane")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Niepoprawne dane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwa ulicy jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 3, ErrorMessage = "Nazwa ulicy musi mieć od 3 do 128 znaków")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Nazwa miejscowości musi mieć od 2 do 128 znaków")]
        public string City { get; set; }
        public string Nip { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsBusiness)
            {
                Regex nipValidation = new(@"^((\d{3}[- ]\d{3}[- ]\d{2}[- ]\d{2})|(\d{3}[- ]\d{2}[- ]\d{2}[- ]\d{3}))$");
                if(string.IsNullOrWhiteSpace(Nip))
                    yield return new ValidationResult("Numer NIP jest wymagany", new[] { nameof(Nip) });
                else if(!nipValidation.IsMatch(Nip))
                {
                    yield return new ValidationResult("Numer nip nie jest prawidłowy (przykład: 123-45-67-819)", 
                                                        new[] { nameof(Nip) });
                }
            }
        }
    }
}
