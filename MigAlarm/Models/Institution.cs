using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public class Institution
    {
        public Institution()
        {
            Roles = new HashSet<Role>();
            Notifications = new HashSet<Notification>();
        }

        [Key]
        public int Id { get; set; }       

        [DisplayName("Nazwa"), Required]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}