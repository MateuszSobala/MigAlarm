using System.Web.Mvc;
using System.Web.Security;
using MigAlarm.Helpers;
using MigAlarm.Models;

namespace MigAlarm.Controllers
{
    public class UserController : Controller
    {
        private MigAlarmContext db = new MigAlarmContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel logon)
        {
            logon.RedirectUrl = "~/";
            // Verify the fields.
            if (ModelState.IsValid)
            {
                // Authenticate the user.
                if (IdentityHelper.ValidateUser(logon, Response))
                {
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
