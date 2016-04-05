using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigAlarm.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNo { get; set; }
        public int? FlatNo { get; set; }

        [Required]
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual Institution Institution { get; set; }
    }
}