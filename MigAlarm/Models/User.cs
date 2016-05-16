using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MigAlarm.Models
{
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Notifications = new HashSet<Notification>();
        }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [DisplayName("Imię")]
        public string Forname { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        [DisplayName("Adres email")]
        public string Email { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogin { get; set; }

        [Required, DefaultValue(false)]
        public bool IsLoggedIn { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        [NotMapped]
        [DisplayName("Aktywne zdarzenia")]
        public int ActiveEventsCounter => Notifications.Count(x => x.DateAccepted.HasValue && !x.DateClosed.HasValue);
    }
}