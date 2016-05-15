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
        Name = 1,
        [Display(Name = "Rok urodzenia: ")]
        Birthday,
        [Display(Name = "Miejsce zamieszkania: ")]
        HomeAddress,
        [Display(Name = "Choroby: ")]
        Diseases,
        [Display(Name = "Przyjmowane leki: ")]
        Medicines,
        [Display(Name = "Grupa krwi: ")]
        BloodGroup,
        [Display(Name = "Wygląd: ")]
        Appearance,
        [Display(Name = "Numer telefonu: ")]
        PhoneNumber,
        [Display(Name = "Obecna lokalizacja: ")]
        Localization,
        [Display(Name = "Skype: ")]
        Skype,
        [Display(Name = "Zdjęcie z miejsca zdarzenia: ")]
        Image,
        [Display(Name = "Inne: ")]
        Other = 100
    }
}