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

        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentEventId { get; set; }

        [ForeignKey("ParentEventId")]
        public Event ParentEvent { get; set; }
        public ICollection<Event> ChildEvents { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public ICollection<EventNode> Ancestors { get; set; }
        public ICollection<EventNode> Offspring { get; set; }

        /**
         * In order to get all ancestors or offspring the EventNode entity was provided.
         * 
         * It can be used in a following way:
         * IEnumerable<Event> allParentsOfRobbery = 
         *      dbContext.Set<Event>().Find(ev => ev.Name == "Robbery").Ancestors.Select(x => x.Ancestor);
         */
    }
}