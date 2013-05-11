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
using Sunshine.Models;
using Sunshine.Business.Account;
using Sunshine.Business.Security;

namespace Sunshine.Controllers
{
    [Authorize(Roles="Security")]
    [InitializeSimpleMembership]
    public class SecurityController : BaseController
    {
        readonly int pageSize = 25;
        private UsersContext db = new UsersContext();


        public SecurityController()
        {
            ViewBag.ModuleName = "后台管理";
        }

        public ActionResult Index()
        {
            return RedirectToAction("Admins");
        }

        public ActionResult Admins(int? pageIndex)
        {
            ViewBag.CurrentModule = "Admins";
            int skipNumber = pageSize * ((pageIndex ?? 1) - 1);
            var Users = AccountManager.Current.GetAdminUsers(skipNumber, skipNumber+ pageSize);
            ViewBag.CurrentPageIndex = pageIndex ?? 1;
            ViewBag.IsLastPage = Users.Count < pageSize;
            return View(Users);
        }

        public ActionResult NewAdmin()
        {
            ViewBag.CurrentModule = "Admins";
            return View();
        }

        [HttpPost]
        public ActionResult NewAdmin(string userName, params string[] roles)
        {
            try
            {
                AccountManager.Current.AddNewUser(userName, true);
                WebSecurity.CreateAccount(userName, userName);
                if (roles!=null && roles.Length > 0) Roles.AddUserToRoles(userName, roles);

                return RedirectToAction("Admins");
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult EditAdmin(int? id)
        {
            ViewBag.CurrentModule = "Admins";
            return View(new User(id??0));
        }
        [HttpPost]
        public ActionResult EditAdmin(string userName, params string[] roles)
        {
            try
            {
                var allRoles =  Roles.GetRolesForUser(userName);
                var list = roles.Except(allRoles);
                var exclude = allRoles.Except(roles);
                if (!exclude.Contains("Security") || Roles.GetUsersInRole("Security").Count() > 1)
                {
                    if (list != null && list.Any()) Roles.AddUserToRoles(userName, list.ToArray());
                    if (exclude != null && exclude.Any())
                    {

                        Roles.RemoveUserFromRoles(userName, exclude.ToArray());
                    }
                }
                return RedirectToAction("Admins");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete(int userId)
        {
            return View();
        }

        public ActionResult ListRoles()
        {
            ViewBag.CurrentModule = "ListRoles";
            var result = from s in Roles.GetAllRoles()
                   select new RoleModel(){RoleName=s};
            return View(result);
        }

        public ActionResult UsersInRole(string role, int? pageIndex)
        {
            ViewBag.CurrentModule = "ListRoles";
            int skipNumber = 100 * pageIndex ?? 0;
            const string query = @"select u.UserName, r.RoleName, c.CompanyName, u.UserId from [User] u 
join dbo.webpages_UsersInRoles ur
on u.UserId = ur.UserId
join dbo.webpages_Roles r
on ur.RoleId = r.RoleId
left join Company c
on u.CompanyId = c.CompanyId
where r.RoleName = @rolename and u.UserName <> 'admin' 
order by u.UserId";
            var result = db.Database.SqlQuery<UserModel>(query, new SqlParameter("rolename", role)).Skip(skipNumber).Take(100);
            return View(result);
        }

        public JsonResult RemoveFromRole(string userName, string roleName)
        {
            try
            {
                if ("security".Equals(roleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (Roles.GetUsersInRole("security").Length <= 1)
                    {
                        return Json(new ResponseResult { Status = ResponseResultStatus.Failed, Message = "至少需要一个安全管理员" });
                    }
                }
                Roles.RemoveUserFromRole(userName, roleName);
                return Json(new ResponseResult { Status = ResponseResultStatus.Sucess, Message = "删除成功" });
            }
            catch
            {
                return Json(new ResponseResult { Status = ResponseResultStatus.Failed, Message = "删除失败" });
            }
        }

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

        const string querybyrole = @"select u.UserName, r.RoleName, c.CompanyName, u.UserId from [User] u 
join dbo.webpages_UsersInRoles ur
on u.UserId = ur.UserId
join dbo.webpages_Roles r
on ur.RoleId = r.RoleId
left join Company c
on u.CompanyId = c.CompanyId
where r.RoleName = @rolename and u.UserName <> 'admin' 
order by u.UserId";
        const string queryAllUsers = @"select u.UserName, r.RoleName, c.CompanyName, u.UserId from [User] u 
join dbo.webpages_UsersInRoles ur
on u.UserId = ur.UserId
join dbo.webpages_Roles r
on ur.RoleId = r.RoleId
left join Company c
on u.CompanyId = c.CompanyId
order by u.UserId";
        
        public JsonResult GetUsers(string roleName, int? pageIndex)
        {
            int skipNumber = 100 * pageIndex ?? 0;

            List<UserModel> result;
            if(!string.IsNullOrEmpty(roleName))
                result = db.Database.SqlQuery<UserModel>(querybyrole, new SqlParameter("rolename", roleName)).Skip(skipNumber).Take(100).ToList();
            else
                result = db.Database.SqlQuery<UserModel>(queryAllUsers).Skip(skipNumber).Take(100).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
