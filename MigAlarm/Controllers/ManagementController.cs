using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MigAlarm.Helpers;
using MigAlarm.Models;
using MigAlarm.Models.Views;
using PagedList;
using static System.String;

namespace MigAlarm.Controllers
{
    public class ManagementController : Controller
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();
        private readonly MailHelper _mailClient = new MailHelper();

        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var currentUser = IdentityHelper.User;
            var selectedInstitution = Request.Cookies["institution"] != null ? int.Parse(Request.Cookies["institution"].Value) : -1;

            var users =
                _db.Users.Where(
                    u => u.Roles.FirstOrDefault(r => r.RoleType == RoleType.User && r.InstitutionId == selectedInstitution) != null && u.UserId != currentUser.UserId)
                    .ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Surname.ToLower().Contains(searchString) || s.Forname.ToLower().Contains(searchString)).ToList();
            }

            var pageSize = users.Count / 10 == 0 ? 10 : users.Count / 10;
            var pageNumber = page ?? 1;

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Forname,Surname,Email")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Cookies["institution"] == null)
                    {
                        throw new Exception("Skontaktuj się z administratorem systemu.");
                    }

                    if (_db.Users.FirstOrDefault(x => x.Email.Equals(user.Email)) != null)
                    {
                        throw new Exception("W systemie istnieje użytkownik z podanym adresem email.");
                    }

                    user.Password = System.Web.Security.Membership.GeneratePassword(8, 4);
                    user.Roles.Add(new Role
                    {
                        Institution = _db.Institutions.Find(int.Parse(Request.Cookies["institution"].Value)),
                        RoleType = RoleType.User
                    });

                    _db.Users.Add(user);

                    _mailClient.Send(user.Email, "Hasło do aplikacji MigAlarm", $"Twoje hasło to: {user.Password}", false);

                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(user);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request.Cookies["institution"] == null)
            {
                throw new Exception("Skontaktuj się z administratorem systemu.");
            }

            var userToUpdate = _db.Users.Find(id);

            if(!userToUpdate.Roles.Any(x => x.InstitutionId == int.Parse(Request.Cookies["institution"].Value) && x.RoleType == RoleType.User))
            {
                throw new Exception("Nie masz uprawnień do edycji tego użytkownika.");
            }

            if (TryUpdateModel(userToUpdate, "", new [] { "Forname", "Surname"}))
            {
                try
                {
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Wystąpił błąd, spróbuj ponownie.");
                }
            }

            return View(userToUpdate);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Wystąpił błąd. Spróbuj ponownie bądź skonsultuj się z administratorem.";
            }

            var user = _db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _db.Users.Find(id);

                if (!user.Roles.Any(x =>
                {
                    var httpCookie = Request.Cookies["institution"];
                    return httpCookie != null && (x.InstitutionId == int.Parse(httpCookie.Value) &&
                                                                      x.RoleType == RoleType.User);
                }))
                {
                    throw new Exception("Nie masz uprawnień do usunięcia tego użytkownika.");
                }

                _db.Users.Remove(user);

                _db.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Wystąpił błąd, spróbuj ponownie.");
            }
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult GetDetails(int id)
        {
            var user = _db.Users.Find(id);

            var model = new UserDetailsViewModel
            {
                Username = $"{user.Forname} {user.Surname}",
                IsOnline = user.IsLoggedIn,
                LastLoginDate = user.LastLogin,
                ActiveEventsCounter =
                    _db.Notifications.Count(x => x.UserId == id && x.DateAccepted.HasValue && !x.DateClosed.HasValue),
                ClosedEventsCounter = 
                    _db.Notifications.Count(x => x.UserId == id && x.DateAccepted.HasValue && x.DateClosed.HasValue)
            };

            return PartialView("_Details", model);
        }

        [Authorize]
        public ActionResult ResetPassword(int id)
        {
            try
            {
                var user = _db.Users.Find(id);

                user.Password = System.Web.Security.Membership.GeneratePassword(8, 4);

                _mailClient.Send(user.Email, "Zmiana hasła w aplikacji MigAlarm", $"Twoje nowe hasło to: {user.Password}", false);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("GetDetails", id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
