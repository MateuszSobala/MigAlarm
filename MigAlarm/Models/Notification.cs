using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class Notification
    {
        public Notification()
        {
            AdditionalData = new HashSet<AdditionalData>();
        }

        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateAccepted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateClosed { get; set; }

        [Required]
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [Required]
        public virtual Coordinate Coordinate { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<AdditionalData> AdditionalData { get; set; }
    }
}