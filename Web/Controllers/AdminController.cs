using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Filters;
using Sunshine.Business.Core;
using WebMatrix.WebData;

namespace Sunshine.Controllers
{
    [Authorize(Users="admin")]
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Admin/

        public AdminController()
        {
            ViewBag.ModuleName = "后台管理";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Accounts()
        {
            var Users = db.Users.Take(100).ToList();
            return View(Users);
        }

    }
}
