using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;

namespace Sunshine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10)]
        public ActionResult LatestProducts(int? categoryId)
        {
            try
            {
                using (UsersContext ctx = new UsersContext())
                {
                    if (categoryId == null)
                        return View(ctx.Products.OrderByDescending(a => a.ProductId).Take(ItemSize).ToList());
                    return View(ctx.Products.Where(a => a.CategoryId == categoryId).OrderByDescending(a => a.ProductId).Take(ItemSize).ToList());
                }
               
            }
            catch
            {
                return View(new List<Product>());
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10)]
        public ActionResult LatestCompanies()
        {
            try
            {
                using (UsersContext ctx = new UsersContext())
                {
                    return View(ctx.Companys.OrderByDescending(a => a.CompanyId).Take(ItemSize).ToList());
                }

            }
            catch
            {
                return View();
            }
        }

        public readonly int ItemSize = 5;
    }
}
