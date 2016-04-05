using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public int InstitutionId { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNo { get; set; }
        public int? FlatNo { get; set; }

        [Required]
        public virtual Country Country { get; set; }
        public virtual Institution Institution { get; set; }
    }
}