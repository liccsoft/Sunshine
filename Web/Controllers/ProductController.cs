using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Filters;
using WebMatrix.WebData;

namespace Sunshine.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ProductController : Controller
    {
        private UsersContext db = new UsersContext();

        public ProductController()
            : base()
        {
            ViewBag.ModuleName = "产品管理";
        }
        //
        // GET: /Product/

        public ActionResult Index()
        {
            //var product = db.Products.ToDictionary<Product, long>((a) => { return a.ProductId; });
            //// product.Add(0, new Product() { ProductName = "无" });
            //ViewData["Product"] = product;
            return View(db.Products.Where(a=>a.UserId == Utility.CurrentUser.UserId).ToList());
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
        //
        // GET: /Product/Details/5


        public ActionResult Details(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [AllowAnonymous]
        public ActionResult Display(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            SetDefaultInfo(new Product());
            return View();
        }

        public JsonResult ExtraProperty(int? productTypeId)
        {
            //var Properties = ProductManager.GetPropertiesForProductType(productTypeId);
            return Json("");//Properties);
        }

        //
        // POST: /Product/Create

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

        //
        // GET: /Product/Edit/5

        // [Authorize(Roles = "edit")]
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

        //
        // POST: /Product/Edit/5

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

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}