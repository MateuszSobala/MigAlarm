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
            public int Name { get; set; }
            public int YearOfBirth { get; set; }
            public string Address { get; set; }
            public string Diseases { get; set; }
            public string Other { get; set; }
        }

        public class Localization
        {
            public string Address { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}