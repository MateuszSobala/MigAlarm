using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models.Views
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Hasło musi mieć conajmniej 8 znaków, maksymalnie 30")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Podane hasła nie są identyczne.")]
        public string RepeatedPassword { get; set; }
    }
}