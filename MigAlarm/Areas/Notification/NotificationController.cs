using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using MigAlarm.Models;

namespace MigAlarm.Areas.Nofitication
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
                Location =
                    DbGeography.FromText("POINT(" + json.Localizations.Latitude + " " + json.Localizations.Longitude + ")")
            };

            var institution = GetNearestInstitution(coordinate.Location);

            var notification = new Notification {EventId = json.EventId};

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

            var userCurrentAddress = new AdditionalData
            {
                AdditionalDataType = AdditionalDataType.Appearance,
                Text = json.Localizations.Address,
                Notification = notification
            };

            return Json("Success");
        }

        private class Difference : IEquatable<Difference>
        {
            public int institutionId;
            public DbGeography difference;
            public bool Equals(Difference other)
            {
                throw new NotImplementedException();
            }
        }

        private Institution GetNearestInstitution(DbGeography currentLocalization)
        {
            var differenceList = new List<Difference>();
            Parallel.ForEach(_db.Institutions, (currentInstitution) =>
            {
                var difference = currentLocalization.Difference(currentInstitution.Coordinate.Location);
                
                var diffObj = new Difference
                {
                    institutionId = currentInstitution.Id,
                    difference = difference
                };

                differenceList.Add(diffObj);
            });

            var sortedDiff = differenceList.OrderBy(l => l.difference).ToList();
            var nearestInstotution = _db.Institutions.Find(sortedDiff.First());

            return nearestInstotution;
        }
    }
}
