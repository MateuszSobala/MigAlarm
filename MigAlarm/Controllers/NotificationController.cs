using System.Linq;
using System.Web.Mvc;
using MigAlarm.Models;

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
            var events = _db.Events.Where(x => x.)

            return PartialView("NewEvents");
        }

        [Authorize]
        public ActionResult GetActiveNotifications()
        {
            return PartialView("ActiveEvents");
        }

        [Authorize]
        public ActionResult GetDoneNotifications()
        {
            return PartialView("DoneEvents");
        }
    }
}