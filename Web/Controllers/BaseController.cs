using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.ViewModels;
using System.Xml.Serialization;
using System.IO;
using Sunshine.Business.Core;

namespace Sunshine.Controllers
{
    public class BaseController : Controller
    {
        public User CurrentUser
        {
            get
            {
                return Utility.CurrentUser;
            }
        }

        List<ModuleModel> AdminModules {
            get {
                return Utility.ModulesManager.AdminModules;
            }
        }
        
        static BaseController()
        {
            
            //AdminModules = new List<ModuleModel>();
            //AdminModules.Add(new ModuleModel { ControllerName = "Security", ActionName = "Admins", ModuleName = "Admins", Title = "管理员管理", AllowRole = "Security" });
            //AdminModules.Add(new ModuleModel { ControllerName = "Manage", ActionName = "Users", ModuleName = "Users", Title = "用户管理", AllowRole = "Account" });
            //AdminModules.Add(new ModuleModel { ControllerName = "Security", ActionName = "ListRoles", ModuleName = "ListRoles", Title = "角色管理", AllowRole = "Security" });
            //AdminModules.Add(new ModuleModel { ControllerName = "Manage", ActionName = "Companies", ModuleName = "Companies", Title = "公司管理", AllowRole = "Company" });
            //AdminModules.Add(new ModuleModel { ControllerName = "Category", ActionName = "Index", ModuleName = "Category", Title = "类别管理", AllowRole = "Category" });

        }

        public BaseController()
        {
            ViewData["modules"] = AdminModules;
            ViewData["UserModules"] = Utility.ModulesManager.UserModules;
        }

        public ActionResult NotAllow(string message)
        {
            ViewBag.ErrorMessage = message;
            return View("Error");
        }

        public ActionResult ToErrorPage(string message)
        {
            ViewBag.ErrorMessage = message;
            return View("Error");
        }

    }
}
