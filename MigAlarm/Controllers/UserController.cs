using System;
using System.Web.Mvc;
using System.Web.Security;
using MigAlarm.Helpers;
using MigAlarm.Models;
using System.Linq;
using MigAlarm.Models.Views;

namespace MigAlarm.Controllers
{
    public class UserController : Controller
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();

        [HttpGet]
        public ActionResult Login()
        {
            var allInstitutions = _db.Institutions.Select(x => new Item { Id = x.Id, Name = x.Name }).ToList();
            var logon = new LoginViewModel(allInstitutions)
            {
                SelectedInstitutionId =
                    Request.Cookies["institution"] != null ? int.Parse(Request.Cookies["institution"].Value) : 0
            };
            return View(logon);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel logon)
        {
            var allInstitutions = _db.Institutions.Select(x => new Item { Id = x.Id, Name = x.Name }).ToList();
            logon._institutions = allInstitutions;

            if (!ModelState.IsValid)
            {
                return View(logon);
            }

            logon.RedirectUrl = "~/";
            var identityResult = IdentityHelper.ValidateUser(logon, Response);
            if (identityResult.Count == 0)
            {
                TempData["Institution"] = logon.SelectedInstitutionId;

                var user = _db.Users.Find(IdentityHelper.User.UserId);
                user.IsLoggedIn = true;
                user.LastLogin = DateTime.Now;
                _db.SaveChanges();

                if (string.IsNullOrWhiteSpace(logon.RedirectUrl))
                {
                    logon.RedirectUrl = "~/User/Login";
                }

                return Redirect(logon.RedirectUrl);
            }

            foreach (var item in identityResult)
            {
                ModelState.AddModelError(item.Key, item.Value);
            }

            return View(logon);
        }

        public ActionResult Logout()
        {
            var user = _db.Users.Find(IdentityHelper.User.UserId);
            user.IsLoggedIn = false;

            FormsAuthentication.SignOut();

            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.Find(IdentityHelper.User.UserId);

                user.Password = model.Password;

                _db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
