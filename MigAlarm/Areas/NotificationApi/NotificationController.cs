using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MigAlarm.Models;
using MigAlarm.Utils;
using System.Data.Entity;

namespace MigAlarm.Areas.NotificationApi
{
    public class NotificationController : ApiController
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();

        private readonly int _otherEventId = 300;

        [HttpPost]
        public IHttpActionResult AddNotification(JsonReceiveItem json)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coordinate = new Coordinate
            {
                Location = GeoUtils.CreatePoint(json.Localizations.Latitude, json.Localizations.Longitude)
            };

            var nearestInstitution = GetNearestInstitution(coordinate.Location);

            var notificationEvent = _db.Events.Find(json.EventId);
            if (notificationEvent == null)
            {
                if (string.IsNullOrWhiteSpace(json.EventName))
                    return BadRequest("Invalid Event");

                notificationEvent = _db.Events.Find(_otherEventId);
            }

            var notification = new Notification
            {
                Event = notificationEvent,
                EventId = notificationEvent.EventId,
                Coordinate = coordinate,
                Institution = nearestInstitution
            };

            var additionalData = FillInAdditionalData(json, ref notification);

            _db.Coordinates.Add(coordinate);
            _db.Notifications.Add(notification);
            _db.AdditionalData.AddRange(additionalData);
            _db.SaveChanges();

            return Json("Sukces! Najbliższa instytucja, to " + nearestInstitution.Name + " znajdująca się pod adresem: " + nearestInstitution.Address.Street + " " + nearestInstitution.Address.HouseNo + ", " + nearestInstitution.Address.ZipCode + " " + nearestInstitution.Address.City + " " + nearestInstitution.Address.Country.Name);
        }

        private static IEnumerable<AdditionalData> FillInAdditionalData(JsonReceiveItem json, ref Notification notification)
        {
            var additionalData = new List<AdditionalData>();

            var userName = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.Name,
                Text = json.Users.Name,
                Notification = notification
            };
            additionalData.Add(userName);

            var phoneNumber = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.PhoneNumber,
                Text = json.Users.PhoneNumber,
                Notification = notification
            };
            additionalData.Add(phoneNumber);


            if (!string.IsNullOrWhiteSpace(json.Users.YearOfBirth))
            {
                var userYearOfBirth = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Birthday,
                    Text = json.Users.YearOfBirth,
                    Notification = notification
                };
                additionalData.Add(userYearOfBirth);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.Address))
            {
                var userAddress = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.HomeAddress,
                    Text = json.Users.Address,
                    Notification = notification
                };
                additionalData.Add(userAddress);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.Diseases))
            {
                var userDiseases = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Diseases,
                    Text = json.Users.Diseases,
                    Notification = notification
                };
                additionalData.Add(userDiseases);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.Medicines))
            {
                var userMedicines = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Medicines,
                    Text = json.Users.Medicines,
                    Notification = notification
                };
                additionalData.Add(userMedicines);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.Appearance))
            {
                var userAppearance = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Appearance,
                    Text = json.Users.Appearance,
                    Notification = notification
                };
                additionalData.Add(userAppearance);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.BloodGroup))
            {
                var userBloodGroup = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.BloodGroup,
                    Text = json.Users.BloodGroup,
                    Notification = notification
                };
                additionalData.Add(userBloodGroup);
            }

            if (!string.IsNullOrWhiteSpace(json.Localizations.Address))
            {
                var localization = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Localization,
                    Text = json.Localizations.Address,
                    Notification = notification
                };
                additionalData.Add(localization);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.Skype))
            {
                var userSkype = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Skype,
                    Text = json.Users.Skype,
                    Notification = notification
                };
                additionalData.Add(userSkype);
            }

            if (!string.IsNullOrWhiteSpace(json.Photo))
            {
                var userImage = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Image,
                    Text = json.Photo,
                    Notification = notification
                };
                additionalData.Add(userImage);
            }

            if (!string.IsNullOrWhiteSpace(json.EventName))
            {
                var  customEvent = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.CustomEvent,
                    Text = json.EventName,
                    Notification = notification
                };
                additionalData.Add(customEvent);
            }

            if (!string.IsNullOrWhiteSpace(json.Users.Other))
            {
                var other = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Other,
                    Text = json.Users.Other,
                    Notification = notification
                };
                additionalData.Add(other);
            }

            return additionalData;
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
