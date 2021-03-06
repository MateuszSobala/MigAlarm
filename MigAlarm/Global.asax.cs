﻿
using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using MigAlarm.Helpers;
using MigAlarm.Models;

namespace MigAlarm
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<MigAlarmContext>(null);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;
            // Get the forms authentication ticket.
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null) return;
            var identity = new GenericIdentity(authTicket.Name, "Forms");
            var principal = new AppPrincipal(identity);

            // Get the custom user data encrypted in the ticket.
            var userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;

            // Deserialize the json data and set it on the custom principal.
            var serializer = new JavaScriptSerializer();
            principal.User = (User)serializer.Deserialize(userData, typeof(User));

            // Set the context user.
            Context.User = principal;
        }
    }
}
