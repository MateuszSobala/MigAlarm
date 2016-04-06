using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MigAlarm.Models
{
    public class LoginViewModel
    {
        private readonly List<Item> _institutions;

        public LoginViewModel(List<Item> institutions)
        {
            _institutions = institutions;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public int SelectedInstitutionId { get; set; }

        public IEnumerable<SelectListItem> InstitutionItems
        {
            get { return new SelectList(_institutions, "Id", "Name"); }
        }

        public string RedirectUrl { get; set; }
    }
}