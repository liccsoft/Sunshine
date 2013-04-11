using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sunshine.Business.Account;
using WebMatrix.WebData;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Business.Categories;

namespace Sunshine
{
    public static class Utility
    {
        public static string CurrentNickName
        {
            get
            {
                if (HttpContext.Current.Items["CurrentUserNickName"] == null)
                {
                   HttpContext.Current.Items["CurrentUserNickName"] = new AccountManager().GetNickName(WebSecurity.CurrentUserId);
                }

                return HttpContext.Current.Items["CurrentUserNickName"] as string;
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
    }
}