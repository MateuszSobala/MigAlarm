using System.Linq;
using System.Web.Mvc;
using MigAlarm.Helpers;
using MigAlarm.Models;
using MigAlarm.Models.Views;

namespace MigAlarm.Controllers
{
    public class NotificationController : Controller
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetNewNotifications()
        {
            var selectedInstitution = Request.Cookies["institution"] != null ? int.Parse(Request.Cookies["institution"].Value) : -1;

            var notifications = _db.Notifications.Where(x => x.Institution.Id == selectedInstitution && !x.DateAccepted.HasValue && !x.DateClosed.HasValue);

            return PartialView("NewNotifications", new NotificationViewModel { Notifications = notifications.ToList() });
        }

        [Authorize]
        public ActionResult GetActiveNotifications()
        {
            return PartialView("ActiveNotifications");
        }

        [Authorize]
        public ActionResult GetDoneNotifications()
        {
            return PartialView("DoneNotifications");
        }
    }
}