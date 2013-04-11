﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Filters;
using Sunshine.Business.Core;
using WebMatrix.WebData;
using System.Web.Security;
using Sunshine.ViewModels;

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
            var Users = db.Users.Where(a=>a.UserName!="admin").Take(100).ToList();
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

        public ActionResult UsersInRole(string role)
        {
            var result = from s in Roles.GetUsersInRole(role)
                         select new RoleModel() { RoleName = s };
            return View(result);
        }

    }
}
