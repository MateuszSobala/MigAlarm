using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public class AdditionalData
    {
        public int AdditionalDataId { get; set; }
        public int NotificationId { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Text { get; set; }

        public AdditionalDataType AdditionalDataType { get; set; }

        [Required]
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
    }
}