using System;
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

            return PartialView("_NewNotifications", new NotificationViewModel { Notifications = notifications.ToList() });
        }

        [Authorize]
        public ActionResult GetActiveNotifications()
        {
            var selectedInstitution = Request.Cookies["institution"] != null ? int.Parse(Request.Cookies["institution"].Value) : -1;

            var notifications = _db.Notifications.Where(x => x.Institution.Id == selectedInstitution && x.DateAccepted.HasValue && !x.DateClosed.HasValue);

            return PartialView("_ActiveNotifications", new NotificationViewModel {Notifications = notifications.ToList()});
        }

        [Authorize]
        public ActionResult GetDoneNotifications()
        {
            var selectedInstitution = Request.Cookies["institution"] != null ? int.Parse(Request.Cookies["institution"].Value) : -1;

            var notifications = _db.Notifications.Where(x => x.Institution.Id == selectedInstitution && x.DateClosed.HasValue && !x.Rejected);

            return PartialView("_DoneNotifications", new NotificationViewModel { Notifications = notifications.ToList() });
        }

        [Authorize]
        public ActionResult GetRejectedNotifications()
        {
            var selectedInstitution = Request.Cookies["institution"] != null ? int.Parse(Request.Cookies["institution"].Value) : -1;

            var notifications = _db.Notifications.Where(x => x.Institution.Id == selectedInstitution && x.Rejected);

            return PartialView("_RejectedNotifications", new NotificationViewModel { Notifications = notifications.ToList() });
        }

        [Authorize]
        public ActionResult GetDetails(int id)
        {
            var selectedNotification = _db.Notifications.First(x => x.Id == id);

            return PartialView("_Details", new NotificationViewModel { Notification = selectedNotification});
        }

        [Authorize]
        public ActionResult GetConfirm(int id)
        {
            var selectedNotification = _db.Notifications.First(x => x.Id == id);

            return PartialView("_Confirm", new NotificationViewModel { Notification = selectedNotification });
        }

        [Authorize]
        public ActionResult SetActive(int id)
        {
            try
            {
                var notification =
                    _db.Notifications.Include("Event")
                        .Include("Coordinate")
                        .Include("Institution")
                        .First(x => x.Id == id && x.UserId == null && x.DateAccepted == null);
                notification.DateAccepted = DateTime.Now;
                notification.UserId = IdentityHelper.User.UserId;
                _db.SaveChanges();

                return Json(new {Success = "True"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = "False" }, JsonRequestBehavior.AllowGet);

            }
        }

        [Authorize]
        public ActionResult SetClosed(int id)
        {
            try
            {
                var notification =
                    _db.Notifications.Include("Event")
                        .Include("Coordinate")
                        .Include("Institution")
                        .First(x => x.Id == id && x.UserId == IdentityHelper.User.UserId && x.DateAccepted.HasValue);
                notification.DateClosed = DateTime.Now;
                _db.SaveChanges();

                return Json(new { Success = "True" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = "False" }, JsonRequestBehavior.AllowGet);

            }
        }

        [Authorize]
        public ActionResult SetRejected(NotificationViewModel model)
        {
            try
            {
                var notification =
                _db.Notifications.Include("Event")
                    .Include("Coordinate")
                    .Include("Institution")
                    .First(x => x.Id == model.Notification.Id && x.UserId == IdentityHelper.User.UserId && x.DateAccepted.HasValue);
                notification.Comment = model.Notification.Comment;
                notification.Rejected = true;
                notification.DateClosed = DateTime.Now;
                _db.SaveChanges();

                return Json(new { Success = "True", Id = model.Notification.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = "False" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Takeover(int id)
        {
            try
            {
                var notification =
                    _db.Notifications.Include("Event")
                        .Include("Coordinate")
                        .Include("Institution")
                        .First(x => x.Id == id && x.UserId != IdentityHelper.User.UserId && x.DateAccepted.HasValue && !x.DateClosed.HasValue);
                var isFromTheSameInstitution = _db.Users.First(u => u.UserId == IdentityHelper.User.UserId).Roles.Any(r => r.InstitutionId == notification.InstitutionId);

                if (!isFromTheSameInstitution)
                    throw new ArgumentException();

                notification.UserId = IdentityHelper.User.UserId;
                _db.SaveChanges();

                return Json(new { Success = "True" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = "False" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}