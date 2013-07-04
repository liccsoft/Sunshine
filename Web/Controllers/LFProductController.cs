using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Filters;
using WebMatrix.WebData;

namespace Sunshine.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class LFProductController : Controller
    {
        private UsersContext db = new UsersContext();

        public LFProductController()
            : base()
        {
            ViewBag.ModuleName = "求购产品管理";
        }

        public ActionResult Index()
        {
            var product = db.Products.ToDictionary<Product, long>((a) => { return a.ProductId; });
            ViewData["Product"] = product;
            return View(db.Products.ToList());
        }

        public ActionResult Create()
        {
            SetDefaultInfo(new Product());
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.UserId = Utility.CurrentUser.UserId;
                product.Createtime = DateTime.Now;
                product.Updatetime = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(long id = 0)
        {
            Product product = db.Products.Find(id);
            SetDefaultInfo(product);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                product.Updatetime = DateTime.Now;
                product.UserId = Utility.CurrentUser.UserId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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

         [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string pattern, string type, string keywords, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            // ViewData["count"] = search.getcount(pattern, keywords);
            ViewBag.count = Convert.ToInt32(search.getcount(pattern, keywords));
            ViewBag.pages = Convert.ToInt32(search.getcount(pattern, keywords) / 10 + 1);

            ViewBag.keyword = pattern;
            int skipcount = (pageIndex ?? 0) * 10;

            return View(db.Products.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
        }

        private void SetDefaultInfo(Product product)
        {
            ProductManager productmanager = new ProductManager();
            IList<Category> CategoryList = productmanager.getCategory(0);
            ViewData["categorylist"] = Utility.L1CategoryList;

            ViewData["secondcategorylist"] = Utility.L2CategoryList;

            IList<Brand> BrandList = productmanager.getBrand();
            ViewData["brandlist"] = (from s in BrandList
                                     select new SelectListItem
                                     {
                                         Selected = (s.BrandId == product.BrandId),
                                         Text = s.BrandName,
                                         Value = s.BrandId.ToString()
                                     }).ToList();
        }

    }
}
