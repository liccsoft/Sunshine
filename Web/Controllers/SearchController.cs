using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Filters;


namespace Sunshine.Controllers
{

    public class SearchController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        // pattern 关键字
        // type ?
        // keywords
        //pageIndex 页数
        [AllowAnonymous]
        public ActionResult ProductSearch(string pattern, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            string keywords = "1";
            int count = search.getcount(pattern, keywords);
            ViewBag.count = count;
            ViewBag.pages = (count + 9) / 10;
            ViewBag.currentPages = pageIndex??1;
            ViewBag.keyword = pattern;
            int skipcount = ((pageIndex ?? 1)-1) * 10;

            IList<SearchResultItem> results = search.getproductresult(pattern, skipcount);
            return View(results);//Json(new { result = results, status = true });
        }

        public ActionResult ViewDetails(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Merchant()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult MerchantSearch(string pattern, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            string keywords = "2";
            int count = search.getcount(pattern, keywords);
            ViewBag.count = count;
            ViewBag.pages = (count + 9) / 10;
            ViewBag.currentPages = pageIndex ?? 1;
            ViewBag.keyword = pattern;
            int skipcount = ((pageIndex ?? 1) - 1) * 10;

            IList<SearchResultItem> results = search.getmerchantsresult(pattern, skipcount);
            return View(results);//Json(new { result = results, status = true });
        }
        public ActionResult Salesman()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SalesmanSearch(string pattern, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            string keywords = "3";
            int count = search.getcount(pattern, keywords);
            ViewBag.count = count;
            ViewBag.pages = (count + 9) / 10;
            ViewBag.currentPages = pageIndex ?? 1;
            ViewBag.keyword = pattern;
            int skipcount = ((pageIndex ?? 1) - 1) * 10;

            IList<SearchResultItem> results = search.getsalesmanresult(pattern, skipcount);
            return View(results);//Json(new { result = results, status = true });
        }
    }
}
