namespace MigAlarm.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNo { get; set; }
        public int? FlatNo { get; set; }

        public virtual Country Country { get; set; }
    }
}