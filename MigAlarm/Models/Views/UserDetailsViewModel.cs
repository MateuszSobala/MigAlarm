using System.ComponentModel;

namespace MigAlarm.Models.Views
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }

        [DisplayName("Liczba aktywnych zdarzeń")]
        public int ActiveEventsCounter { get; set; }

        [DisplayName("Liczba zamkniętych zdarzeń")]
        public int ClosedEventsCounter { get; set; }
    }
}