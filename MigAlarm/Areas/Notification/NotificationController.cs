using System.Data.Entity.Spatial;
using System.Web.Http;
using MigAlarm.Models;

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
                Location =
                    DbGeography.FromText("POINT(" + json.Localizations.Latitude + " " + json.Localizations.Longitude + ")")
            };

            GetNearestInstitution(coordinate.Location);

            var notification = new Notification {EventId = json.EventId};

            var userName = new AdditionalData
            {
                AdditionalDataType = {Name = "Name"},
                Text = json.Users.Name,
                Notification = notification
            };

            var userYearOfBirth = new AdditionalData
            {
                AdditionalDataType = {Name = "Year of birth"},
                Text = json.Users.YearOfBirth,
                Notification = notification
            };

            var userAddress = new AdditionalData
            {
                AdditionalDataType = {Name = "Address"},
                Text = json.Users.Address,
                Notification = notification
            };

            var userDiseases = new AdditionalData
            {
                AdditionalDataType = {Name = "Diseases"},
                Text = json.Users.Diseases,
                Notification = notification
            };

            var other = new AdditionalData
            {
                AdditionalDataType = {Name = "Other"},
                Text = json.Users.Other,
                Notification = notification
            };

            var userCurrentAddress = new AdditionalData
            {
                AdditionalDataType = {Name = "Localization address"},
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
