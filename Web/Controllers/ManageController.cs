using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;

namespace Sunshine.Controllers
{
    [Authorize(Roles="GeneralManage,AccountManager,CompanyManager")]
    public class ManageController : BaseController
    {
        const int pageSize = 20;
        UsersContext db = new UsersContext();
        public ManageController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "CompanyManager")]
        public ActionResult Companies(int? pageIndex)
        {
            ViewBag.CurrentModule = "Companies";
            return View(new UsersContext().Companys.ToList());
        }
        [Authorize(Roles = "AccountManager")]
        public ActionResult Users(int? pageIndex)
        {
            ViewBag.CurrentModule = "Users";
            int skipNumber = pageSize * ((pageIndex ?? 1) - 1);
            var Users = db.Users.Where(a => !a.IsAdmin).OrderBy(a => a.UserId).Skip(skipNumber).Take(pageSize).ToList();
            ViewBag.CurrentPageIndex = pageIndex ?? 1;
            ViewBag.IsLastPage = Users.Count < pageSize;
            return View(Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                disposing = true;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
