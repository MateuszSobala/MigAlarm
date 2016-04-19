using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MigAlarm.Models.Views
{
    public class LoginViewModel
    {
        private readonly List<Item> _institutions;

        public LoginViewModel() { }

        public LoginViewModel(List<Item> institutions)
        {
            _institutions = institutions;
        }

        [Required]
        [Display(Name = "Adres email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Instytucja")]
        public int SelectedInstitutionId { get; set; }

        public IEnumerable<SelectListItem> InstitutionItems => _institutions != null ? new SelectList(_institutions, "Id", "Name") : null;

        public string RedirectUrl { get; set; }
    }
}