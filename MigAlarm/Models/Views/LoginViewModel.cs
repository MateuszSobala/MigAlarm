using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MigAlarm.Models.Views
{
    public class LoginViewModel
    {
        public List<Item> _institutions;

        public LoginViewModel() { }

        public LoginViewModel(List<Item> institutions)
        {
            _institutions = institutions;
        }

        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Adres email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Instytucja")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int SelectedInstitutionId { get; set; }

        public IEnumerable<SelectListItem> InstitutionItems => _institutions != null ? new SelectList(_institutions, "Id", "Name") : null;

        public string RedirectUrl { get; set; }
    }
}