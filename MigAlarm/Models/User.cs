using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [DisplayName("Imię")]
        public string Forname { get; set; }

        [Required]
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [DisplayName("Adres email")]
        public string Email { get; set; }
    }
}