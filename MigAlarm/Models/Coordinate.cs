using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace MigAlarm.Models
{
    public class Coordinate
    {
        [Key]
        public int Id { get; set; }
        public DbGeography Location { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual Notification Notification { get; set; }
    }
}