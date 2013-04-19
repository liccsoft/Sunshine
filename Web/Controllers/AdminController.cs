using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Filters;
using Sunshine.Business.Core;
using WebMatrix.WebData;
using System.Web.Security;
using Sunshine.ViewModels;
using System.Data.SqlClient;

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

        public ActionResult Accounts(int? pageIndex)
        {
            int skipNumber = 20 * pageIndex??0;
            var Users = db.Users.OrderBy(a=>a.UserId).Skip(skipNumber).Take(20).ToList();
            return View(Users);
        }

        [HttpPost]
        public ActionResult Delete(int userId)
        {
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration=1000)]
        public ActionResult ListRoles()
        {
            var result = from s in Roles.GetAllRoles()
                   select new RoleModel(){RoleName=s};
            return View(result);
        }

        public ActionResult UsersInRole(string role, int? pageIndex)
        {
            int skipNumber = 20 * pageIndex ?? 0;
            const string query = @"select u.UserName, r.RoleName, c.CompanyName, u.UserId from [User] u 
join dbo.webpages_UsersInRoles ur
on u.UserId = ur.UserId
join dbo.webpages_Roles r
on ur.RoleId = r.RoleId
left join Company c
on u.CompanyId = c.CompanyId
where r.RoleName = @rolename order by u.UserId";
            var result = db.Database.SqlQuery<UserRoleModel>(query, new SqlParameter("rolename", role)).Skip(skipNumber).Take(20);
            return View(result);
        }

    }
}
