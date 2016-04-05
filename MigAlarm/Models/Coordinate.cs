using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

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