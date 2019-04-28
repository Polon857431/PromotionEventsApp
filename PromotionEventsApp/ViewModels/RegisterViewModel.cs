using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole Imię wymagane")]
        [UIHint("first name")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole Nazwisko wymagane")]
        [UIHint("last name")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email - pole wymagane")]
        [EmailAddress]
        [UIHint("email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło - pole wymagane")]
        [UIHint("password")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Powtórz Hasło - pole wymagane")]
        [Compare("Password", ErrorMessage = "Hasła się różnią, spróbuj jeszcze raz")]
        [UIHint("password")]
        [Display(Name = "Powtórz Hasło")]
        public string PasswordRepeat { get; set; }
    }
}
