using System.Data.Entity.Spatial;
using System.Web.Http;
using MigAlarm.Models;
using MigAlarm.Utils;

namespace MigAlarm.Areas.Nofitication
{
    public class NotificationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddNotification(JsonReceiveItem json)
        {
            if (json == null) return Json("An Error Has occoured");
            var coordinate = new Coordinate
            {
                Location = GeoUtils.CreatePoint(json.Localizations.Latitude, json.Localizations.Longitude)
            };

            GetNearestInstitution(coordinate.Location);

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

        private static Institution GetNearestInstitution(DbGeography currentLocalization)
        {
            return new Institution();
        }
    }
}
