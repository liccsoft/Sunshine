using System.Web.Mvc;
using Sunshine.Business.Search;

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

        public ActionResult Goods(string name, int? pageIndex, string field = "companyname")
        {
            var results = SearchEngine.Search(name, field ?? "companyname", "product", new  SearchOptions{ CurrentIndex = pageIndex ?? 1});
            ViewBag.Name = name;
            return View(results);
        }

    }
}
