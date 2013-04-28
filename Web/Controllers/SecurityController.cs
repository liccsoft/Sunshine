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

namespace Sunshine.Controllers
{
    [Authorize(Roles="security")]
    [InitializeSimpleMembership]
    public class SecurityController : Controller
    {
        readonly int pageSize = 5;
        private UsersContext db = new UsersContext();
        //
        // GET: /Admin/

        public SecurityController()
        {
            ViewBag.ModuleName = "后台管理";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Accounts(int? pageIndex)
        {
            int skipNumber = pageSize * ((pageIndex ?? 1) - 1);
            var Users = db.Users.OrderBy(a => a.UserId).Skip(skipNumber).Take(pageSize).ToList();
            ViewBag.CurrentPageIndex = pageIndex ?? 1;
            ViewBag.IsLastPage = Users.Count < pageSize;
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

        public ActionResult FindUser(string userName)
        {
            return View(db.Users.Where(a => a.UserName.Contains(userName)).ToList().ConvertAll<UserRoleModel>((a) => new UserRoleModel { UserName=a.UserName, UserId = a.UserId }));
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
        public ActionResult SecurityGroup()
        {
            return View(db.SecurityGroups.ToList());
        }

        public ActionResult AddEditSecGroup(int? id)
        {

            return View(db.SecurityGroups.Find(id));
        }
        [HttpPost]
        public ActionResult AddEditSecGroup(SecurityGroup securityGroup)
        {

            if (securityGroup.SecurityGroupId > 0)
            {
                new AccountManager().UpdateSecurityGroup(securityGroup);
            }
            else
            new AccountManager().CreateSecurityGroup(securityGroup);
            return RedirectToAction("SecurityGroup");
        }
    }
}
