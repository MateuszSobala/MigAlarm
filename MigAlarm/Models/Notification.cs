using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public sealed class Notification
    {
        public Notification()
        {
            AdditionalData = new HashSet<AdditionalData>();
            DateAdded = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public int? UserId { get; set; }
        public int InstitutionId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime DateAdded { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateAccepted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateClosed { get; set; }

        [Required]
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Required]
        public Coordinate Coordinate { get; set; }

        [ForeignKey("InstitutionId"), Required]
        public Institution Institution { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<AdditionalData> AdditionalData { get; set; }
    }
}