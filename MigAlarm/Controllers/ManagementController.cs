using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MigAlarm.Helpers;
using MigAlarm.Models;
using PagedList;
using static System.String;

namespace MigAlarm.Controllers
{
    public class ManagementController : Controller
    {
        private readonly MigAlarmContext _db = new MigAlarmContext();
        private readonly MailHelper _mailClient = new MailHelper();

        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var users = _db.Users.ToList();

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

            var pageSize = users.Count / 10 == 0 ? 1 : users.Count / 10;
            var pageNumber = page ?? 1;
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Forname,Surname,Email")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Password = System.Web.Security.Membership.GeneratePassword(8, 4);

                    _db.Users.Add(user);

                    _db.SaveChanges();

                    _mailClient.Send(user.Email, "Hasło do aplikacji MigHelp", $"Twoje hasło to: {user.Password}", false);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Wystąpił błąd, spróbuj ponownie.");
            }

            return View(user);
        }

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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userToUpdate = _db.Users.Find(id);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _db.Users.Find(id);

                _db.Users.Remove(user);

                _db.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Wystąpił błąd, spróbuj ponownie.");
            }
            
            return RedirectToAction("Index");
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
