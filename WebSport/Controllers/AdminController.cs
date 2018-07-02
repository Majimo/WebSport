using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSport.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            var roles = new List<string>();
            if (User.IsInRole("Organizer"))
            {
                roles.Add("Organizer");
            }
            if (User.IsInRole("Competitor"))
            {
                roles.Add("Competitor");
            }
            if (User.IsInRole("Visitor"))
            {
                roles.Add("Visitor");
            }
            ViewBag.Roles = roles;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Test()
        {
            return View();
        }
    }
}