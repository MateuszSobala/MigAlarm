using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Forname { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}