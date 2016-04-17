using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public enum AdditionalDataType
    {
        Name = 1, //imie i nazwisko
        Birthday,
        HomeAddress,
        Diseases,
        Medicines, //przyjmowane leki
        BloodGroup,
        Appearance, //wyglad
        PhoneNumber,
        Localization,
        Other = 100
    }
}