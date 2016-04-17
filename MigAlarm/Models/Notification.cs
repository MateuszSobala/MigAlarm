﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MigAlarm.Models
{
    public class Notification
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

        [Column(TypeName = "datetime2"), Required]
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

        [ForeignKey("InstitutionId"), Required]
        public virtual Institution Institution { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<AdditionalData> AdditionalData { get; set; }

        [NotMapped]
        [DisplayName("Adres")]
        public string NotificationAddress
            =>
                AdditionalData.Any(x => x.AdditionalDataType == AdditionalDataType.Localization)
                    ? AdditionalData.First(x => x.AdditionalDataType == AdditionalDataType.Localization).Text
                    : "";
    }
}