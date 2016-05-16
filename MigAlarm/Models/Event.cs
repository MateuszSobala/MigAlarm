using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public class Event
    {
        public Event()
        {
            Notifications = new HashSet<Notification>();
            ChildEvents = new HashSet<Event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Typ zgłoszenia")]
        public string Name { get; set; }

        public int? ParentEventId { get; set; }

        [ForeignKey("ParentEventId")]
        public Event ParentEvent { get; set; }

        public ICollection<Event> ChildEvents { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}