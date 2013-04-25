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
    [Authorize(Roles="admin")]
    [Authorize(Users = "admin")]
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
            int skipNumber = 100 * pageIndex ?? 0;
            const string query = @"select u.UserName, r.RoleName, c.CompanyName, u.UserId from [User] u 
join dbo.webpages_UsersInRoles ur
on u.UserId = ur.UserId
join dbo.webpages_Roles r
on ur.RoleId = r.RoleId
left join Company c
on u.CompanyId = c.CompanyId
where r.RoleName = @rolename order by u.UserId";
            var result = db.Database.SqlQuery<UserRoleModel>(query, new SqlParameter("rolename", role)).Skip(skipNumber).Take(100);
            return View(result);
        }

        [Authorize(Users="admin")]
        public JsonResult RemoveFromRole(string userName, string roleName)
        {
            try
            {
                Roles.RemoveUserFromRole(userName, roleName);
                return Json(new ResponseResult { Status = ResponseResultStatus.Failed, Message = "删除成功" });
            }
            catch
            {
                return Json(new ResponseResult { Status = ResponseResultStatus.Failed, Message = "删除失败" });
            }
        }

        [Authorize(Users = "admin")]
        public JsonResult AddToRole(string userName, string roleName)
        {
            try
            {
                Roles.AddUserToRole(userName, roleName);
                return Json(new ResponseResult { Status = ResponseResultStatus.Failed, Message = "添加成功" });
            }
            catch
            {
                return Json(new ResponseResult { Status = ResponseResultStatus.Failed, Message = "添加失败" });
            }
        }

    }
}
