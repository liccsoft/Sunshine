using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Business.Account;
using Sunshine.ViewModels;

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
            ViewBag.Paging = new PagedModel() { ControllerName = "Manage", ActionName = "Companies", CurrentIndex=pageIndex??1, PagedStype = PagedStyle.PreNext};

            return View(db.Companys.ToList());
        }
        [Authorize(Roles = "Account")]
        public ActionResult Users(int? pageIndex)
        {
            ViewBag.CurrentModule = "Users";
            int skipNumber = pageSize * ((pageIndex ?? 1) - 1);
            var Users = AccountManager.Current.GetNormalUsers(skipNumber, skipNumber + pageSize);
            ViewBag.CurrentPageIndex = pageIndex ?? 1;
            var totalPage = Users.Count < pageSize ? pageIndex ?? 1 : ((pageIndex ?? 1) + 1);
            ViewBag.Paging = new PagedModel() { ControllerName = "Manage", ActionName = "Users", CurrentIndex = pageIndex ?? 1, PagedStype = PagedStyle.PreNext, TotalPage = totalPage };
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
