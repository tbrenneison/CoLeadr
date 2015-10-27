using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoLeadr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "CoLeadr is a web app that facilitates small group organization.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact CoLeadr";

            return View();
        }
    }
}