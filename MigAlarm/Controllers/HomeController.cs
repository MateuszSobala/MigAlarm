using System.Web.Mvc;

namespace MigAlarm.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";  

            return View();
        }
    }
}
