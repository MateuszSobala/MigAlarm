using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MigAlarm.Models;
using MigAlarm.Utils;
using System.Data.Entity;

namespace MigAlarm.Areas.NofiticationApi
{
    public class NotificationController : ApiController
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();

        [HttpPost]
        public IHttpActionResult AddNotification(JsonReceiveItem json)
        {
            if (json == null) return Json("An Error Has occoured");
            var coordinate = new Coordinate
            {
                Location = GeoUtils.CreatePoint(json.Localizations.Latitude, json.Localizations.Longitude)
            };

            var nearestInstitution = GetNearestInstitution(coordinate.Location);

            var notificationEvent = _db.Events.Find(json.EventId);

            var notification = new Notification
            {
                Event = notificationEvent,
                EventId = notificationEvent.EventId,
                Coordinate = coordinate,
                PhoneNumber = json.Users.PhoneNumber,
                Institution = nearestInstitution
            };

            var userName = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.Name,
                Text = json.Users.Name,
                Notification = notification
            };

            var userYearOfBirth = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.Birthday,
                Text = json.Users.YearOfBirth,
                Notification = notification
            };

            var userAddress = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.HomeAddress,
                Text = json.Users.Address,
                Notification = notification
            };

            var userDiseases = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.Diseases,
                Text = json.Users.Diseases,
                Notification = notification
            };

            var other = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.Other,
                Text = json.Users.Other,
                Notification = notification
            };

            _db.Coordinates.Add(coordinate);
            _db.Notifications.Add(notification);
            _db.AdditionalData.AddRange(new List<AdditionalData>() { userName, userYearOfBirth, userAddress, userDiseases, other });
            _db.SaveChanges();

            return Json("Sukces! Najbliższa instytucja, to " + nearestInstitution.Name + " znajdująca się pod adresem: " + nearestInstitution.Address.Street + " " + nearestInstitution.Address.HouseNo + ", " + nearestInstitution.Address.ZipCode + " " + nearestInstitution.Address.City + " " + nearestInstitution.Address.Country.Name);
        }

        private class Difference : IComparable<Difference>
        {
            public int institutionId;
            public double difference;
            public bool Equals(Difference other)
            {
                if (other == null)
                {
                    return false;
                }
                if (institutionId == other.institutionId)
                {
                    return true;
                }
                if (difference == other.difference)
                {
                    return true;
                }
                return false;
            }

            public int CompareTo(Difference other)
            {
                if(Equals(other))
                {
                    return 0;
                }
                if (other == null || difference > other.difference)
                {
                    return 1;
                }
                return -1;
            }
        }

        private Institution GetNearestInstitution(DbGeography currentLocalization)
        {
            var differenceList = new List<Difference>();
            var institutions = _db.Institutions.Include(i => i.Address.Coordinate).ToList();
            Parallel.ForEach(institutions, (currentInstitution) =>
            {
                var difference = currentLocalization.Distance(currentInstitution.Address.Coordinate.Location);
                
                var diffObj = new Difference
                {
                    institutionId = currentInstitution.Id,
                    difference = difference ?? 0
                };

                differenceList.Add(diffObj);
            });

            var nearestId = differenceList.OrderBy(l => l.difference).Select(d => d.institutionId).First();
            var nearestInstitution = _db.Institutions.Include("Address").FirstOrDefault(i => i.Id == nearestId);

            return nearestInstitution;
        }
    }
}
