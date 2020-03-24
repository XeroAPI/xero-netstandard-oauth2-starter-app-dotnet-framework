using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetStandardApp_net461.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            bool firstTimeConnection = false;

            if (TokenUtilities.TokenExists())
            {
                firstTimeConnection = true;
            }

            return View(firstTimeConnection);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}