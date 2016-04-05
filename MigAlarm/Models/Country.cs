using System.Collections.Generic;

namespace MigAlarm.Models
{
    public sealed class Country
    {
        public Country()
        {
            Addresses = new HashSet<Address>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}