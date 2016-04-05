using System.Data.Entity.Spatial;

namespace MigAlarm.Models
{
    public class Coordinate
    {
        public int CoordinateId { get; set; }
        public DbGeography Location { get; set; }
        public int? InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }
    }
}