using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MigAlarm.Models
{
    public enum AdditionalDataType
    {
        [Display(Name = "Imię i nazwisko: ")]
        Name = 1, //imie i nazwisko
        [Display(Name = "Rok urodzenia: ")]
        Birthday,
        [Display(Name = "Miejsce zamieszkania: ")]
        HomeAddress,
        [Display(Name = "Choroby: ")]
        Diseases,
        [Display(Name = "Przyjmowane leki: ")]
        Medicines, //przyjmowane leki
        [Display(Name = "Grupa krwi: ")]
        BloodGroup,
        [Display(Name = "???: ")]
        Appearance, //wyglad
        [Display(Name = "Numer telefonu: ")]
        PhoneNumber,
        [Display(Name = "Obecna lokalizacja: ")]
        Localization,
        [Display(Name = "Inne: ")]
        Other = 100
    }
}