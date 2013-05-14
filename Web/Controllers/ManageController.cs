using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Business.Account;

namespace Sunshine.Controllers
{
    [Authorize(Roles="Manage,Account,Company")]
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
        [Authorize(Roles = "Company")]
        public ActionResult Companies(int? pageIndex)
        {
            ViewBag.CurrentModule = "Companies";
            return View(new UsersContext().Companys.ToList());
        }
        [Authorize(Roles = "Account")]
        public ActionResult Users(int? pageIndex)
        {
            ViewBag.CurrentModule = "Users";
            int skipNumber = pageSize * ((pageIndex ?? 1) - 1);
            var Users = AccountManager.Current.GetNormalUsers(skipNumber, skipNumber + pageSize);
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
