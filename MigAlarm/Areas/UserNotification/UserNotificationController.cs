using System.Collections.Generic;
using System.Web.Http;

namespace MigAlarm.Areas.UserNotification
{
    public class UserNotificationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddNotification(IDictionary<string, object> modelDictionary)
        {
            return Json(modelDictionary != null ? "OK" : "Error");
        }
    }
}
