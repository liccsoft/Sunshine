using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunshine.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneralError(string errorMessage)
        {
            ViewBag.SubModuleName = "错误处理";
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

    }
}
