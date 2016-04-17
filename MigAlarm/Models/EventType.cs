using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public enum EventType
    {
        Police = 1,
        Robbery = 2, // rabunek
        CarAccident = 3, // wypadek samochodowy
        RoadCollision = 4, // kolizja drogowa
        CarAccidentWithVictims = 5, // wypadek z ofiarami, parentId = 3
        FireDepartment = 101, // straż pożarna
        EmergencyService = 201 // pogotowie

    }
}