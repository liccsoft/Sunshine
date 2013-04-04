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
    [Authorize]
    [InitializeSimpleMembership]
    public class ProductController : Controller
    {
        private UsersContext db = new UsersContext();

        public  ProductController():base()
        {
            ViewBag.ModuleName = "产品管理";
        }
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string pattern)
        {
            //var aaa = db.Products.Where(a => a.ProductMark.Contains(pattern));
            //var c = aaa.ToString();
            ViewBag.keyword = pattern;
            return View(db.Products.Where(a=>a.ProductMark.Contains(pattern)).ToList());
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
            ProductManager productmanager = new ProductManager();
            IList<Category> CategoryList = productmanager.getCategoryName();
            ViewData["categorylist"] = (from s in CategoryList
                                       select new SelectListItem
                                       {
                                           Selected = (s.CategoryId == 0),
                                           Text = s.Name,
                                           Value = s.CategoryId.ToString()
                                       }).ToList();

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
               var p = db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}