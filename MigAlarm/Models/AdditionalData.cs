using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public class AdditionalData
    {
        public int AdditionalDataId { get; set; }
        public int AdditionalDataTypeId { get; set; }
        public int NotificationId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [ForeignKey("AdditionalDataTypeId")]
        public virtual AdditionalDataType AdditionalDataType { get; set; }

        [Required]
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
    }
}