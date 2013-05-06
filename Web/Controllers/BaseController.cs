using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunshine.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult NotAllow(string message)
        {
            ViewBag.ErrorMessage = message;
            return View("Error");
        }

        public ActionResult ToErrorPage(string message)
        {
            ViewBag.ErrorMessage = message;
            return View("Error");
        }

    }
}
