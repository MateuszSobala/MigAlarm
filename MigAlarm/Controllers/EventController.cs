using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MigAlarm.Controllers
{
    public class EventController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        } 

    }
}