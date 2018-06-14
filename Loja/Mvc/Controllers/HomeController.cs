using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Lerron Felipe";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Lerron Felipe";

            return View();
        }
    }
}