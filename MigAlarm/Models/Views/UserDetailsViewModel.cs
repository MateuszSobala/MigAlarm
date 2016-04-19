using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models.Views
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }

        public bool IsOnline { get; set; }

        [DisplayName("Ostatnie logowanie")]
        [DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; set; }

        [DisplayName("Liczba aktywnych zdarzeń")]
        public int ActiveEventsCounter { get; set; }

        [DisplayName("Liczba zamkniętych zdarzeń")]
        public int ClosedEventsCounter { get; set; }
    }
}