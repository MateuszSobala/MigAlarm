using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MigAlarm.Models
{
    public class Coordinate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public DbGeography Location { get; set; }

        public virtual Address Address { get; set; }

        public virtual Notification Notification { get; set; }
    }
}