using System.Web.Mvc;
using System.Web.Security;
using MigAlarm.Helpers;
using MigAlarm.Models;
using System.Linq;

namespace MigAlarm.Controllers
{
    public class UserController : Controller
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();

        [HttpGet]
        public ActionResult Login()
        {
            var allInstitutions = _db.Institutions.Select(x => new Item { Id = x.Id, Name = x.Name }).ToList();
            var logon = new LoginViewModel(allInstitutions);
            return View(logon);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel logon)
        {
            logon.RedirectUrl = "~/";
            // Verify the fields.
            if (!ModelState.IsValid) return Redirect(logon.RedirectUrl);
            // Authenticate the user.
            if (IdentityHelper.ValidateUser(logon, Response))
            {
                TempData["Institution"] = logon.SelectedInstitutionId;

                // Redirect to the secure area.
                if (string.IsNullOrWhiteSpace(logon.RedirectUrl))
                {
                    logon.RedirectUrl = "~/User/Login";
                }
            }
            else
            {
                // Invalid email or password or data not in db
                logon.RedirectUrl = "~/User/Login";
            }
            return Redirect(logon.RedirectUrl);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
