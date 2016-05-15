using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public class JsonReceiveItem
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public User Users { get; set; }
        [Required]
        public Localization Localizations { get; set; }
        public string EventName { get; set; }
        public string Photo { get; set; }

        public class User
        {
            [Required]
            public string Name { get; set; }
            public string YearOfBirth { get; set; }
            public string Address { get; set; }
            public string Diseases { get; set; }
            public string Medicines { get; set; }
            public string BloodGroup { get; set; }
            public string Appearance { get; set; }
            [Required]
            public string PhoneNumber { get; set; }
            public string Skype { get; set; }
            public string Other { get; set; }
        }

        public class Localization
        {
            public string Address { get; set; }
            [Required]
            public string Latitude { get; set; }
            [Required]
            public string Longitude { get; set; }
        }
    }
}
 