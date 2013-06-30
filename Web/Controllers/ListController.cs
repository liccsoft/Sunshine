using System.Web.Mvc;
using Sunshine.Business.Search;
using Sunshine.Business.Core;

namespace Sunshine.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /Merchant/

        public ActionResult Merchant(string name, int? id)
        {
            ViewBag.Name = name;
            return View();
        }

        //
        // GET: /Merchant/Details/5

        public ActionResult SaleMan(string name, int? id)
        {
            ViewBag.Name = name;
            return View();
        }

        //
        // GET: /Merchant/Create

        public ActionResult Goods(string name, int? pageIndex, int? pageSize, int? categoryId, string field = "companyname")
        {
            Category category = categoryId == null ? null : Category.Find(categoryId.Value);
            var results = SearchEngine.Search(name, field ?? "companyname", "product", new SearchOptions { CurrentIndex = pageIndex ?? 1, PageSize = pageSize??10, LimitCategory = category});
            ViewBag.Name = name;
            return View(results);
        }

    }
}
