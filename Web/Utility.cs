using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sunshine.Business.Account;
using WebMatrix.WebData;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Business.Categories;
using System.Web.Security;
using Sunshine.ViewModels;

namespace Sunshine
{
    public static class Utility
    {
        public static string CurrentNickName
        {
            get
            {
                return CurrentUser == null ? HttpContext.Current.User.Identity.Name : CurrentUser.NickName;
            }
        }

        public static User CurrentUser
        {
            get
            {
                if (WebSecurity.Initialized && WebSecurity.IsAuthenticated)
                {
                    object o =  HttpContext.Current.Items["CurrentUser"];
                    if(o==null)
                        o= AccountManager.Current.GetUser(WebSecurity.CurrentUserName);
                     HttpContext.Current.Items["CurrentUser"] = o;
                    return o as User;
                }
                return null;
            }
        }

        public static IList<SelectListItem> CompanyList
        {
            get {
                if (HttpContext.Current.Items["CurrentCompanyList"] == null)
                {
                    var temp  = new UsersContext().Companys.ToList().ConvertAll<SelectListItem>((a) => { return new SelectListItem() { Value = a.CompanyId.ToString(), Text = a.CompanyName }; });
                    temp.Insert(0, new SelectListItem() {Value = "0", Text= "未知"});
                    HttpContext.Current.Items["CurrentCompanyList"] = temp;
                }

                return HttpContext.Current.Items["CurrentCompanyList"] as IList<SelectListItem>;
            }
        }

        public static IList<SelectListItem> L1CategoryList
        {
            get {
                DefaultCategoryManager manager=new DefaultCategoryManager();
                var list =manager.GetCategoryByLevel(1);

                return (from c in list select new SelectListItem() { Value = c.CategoryId.ToString(), Text = c.Title }).ToList();
            }
        }

        public static IList<SelectListItem> L2CategoryList
        {
            get
            {
                DefaultCategoryManager manager = new DefaultCategoryManager();
                var list = manager.GetCategoryByLevel(2);

                return (from c in list select new SelectListItem() { Value = c.CategoryId.ToString(), Text = c.Title }).ToList();
            }
        }

        public static IList<SelectListItem> BaseCategoryList
        {
            get
            {
                DefaultCategoryManager manager = new DefaultCategoryManager();
                var list = manager.GetCategoryByLevel(1);
                list.Insert(0, manager.RootCategory);

                return (from c in list select new SelectListItem() { Value = c.CategoryId.ToString(), Text = c.Title }).ToList();
            }
        }

        public static string GetString(string name)
        {
            return Sunshine.Properties.Resources.ResourceManager.GetString(name) ?? name;
        }

        public static ModuleManager ModulesManager
        {
            get {
                if (HttpContext.Current.Cache["ModuleManager"] != null)
                {
                    return (HttpContext.Current.Cache["ModuleManager"] as ModuleManager);
                }
                return null;
            }
        }
    }
}