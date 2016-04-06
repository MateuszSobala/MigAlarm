using System.Collections.Generic;

namespace MigAlarm.Models
{
    public class JsonReceiveItem
    {
        public int EventId { get; set; }
        public User Users { get; set; }
        public Localization Localizations { get; set; }

        public class User
        {
            public string Name { get; set; }
            public string YearOfBirth { get; set; }
            public string Address { get; set; }
            public string Diseases { get; set; }
            public string Other { get; set; }
        }

        public class Localization
        {
            public string Address { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
    }
}