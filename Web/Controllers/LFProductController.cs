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
            var lfproduct = db.LFProduct.ToDictionary<LFProduct, long>((a) => { return a.LFProductId; });
            ViewData["LFProduct"] = lfproduct;
            return View(db.LFProduct.ToList());
        }

        public ActionResult Create()
        {
            SetDefaultInfo(new LFProduct());
            return View();
        }

        [HttpPost]
        public ActionResult Create(LFProduct LFProduct)
        {
            if (ModelState.IsValid)
            {
                LFProduct.UserId = Utility.CurrentUser.UserId;
                LFProduct.Createtime = DateTime.Now;
                LFProduct.Updatetime = DateTime.Now;
                db.LFProduct.Add(LFProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(LFProduct);
        }

        public ActionResult Edit(long id = 0)
        {
            LFProduct LFProduct = db.LFProduct.Find(id);
            SetDefaultInfo(LFProduct);
            if (LFProduct == null)
            {
                return HttpNotFound();
            }
            return View(LFProduct);
        }

        [HttpPost]
        public ActionResult Edit(LFProduct LFProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(LFProduct).State = EntityState.Modified;
                LFProduct.Updatetime = DateTime.Now;
                LFProduct.UserId = Utility.CurrentUser.UserId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(LFProduct);
        }

        public ActionResult Delete(long id = 0)
        {
            LFProduct LFProduct = db.LFProduct.Find(id);
            if (LFProduct == null)
            {
                return HttpNotFound();
            }
            return View(LFProduct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            LFProduct LFProduct = db.LFProduct.Find(id);
            db.LFProduct.Remove(LFProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id = 0)
        {
            LFProduct LFProduct = db.LFProduct.Find(id);
            if (LFProduct == null)
            {
                return HttpNotFound();
            }
            return View(LFProduct);
        }

        [AllowAnonymous]
        public ActionResult Search(string pattern, string type, string keywords, int? pageIndex)
        {
            SearchResultItem search = new SearchResultItem();
            // ViewData["count"] = search.getcount(pattern, keywords);
            ViewBag.count = Convert.ToInt32(search.getcount(pattern, keywords));
            ViewBag.pages = Convert.ToInt32(search.getcount(pattern, keywords) / 10 + 1);

            ViewBag.keyword = pattern;
            int skipcount = (pageIndex ?? 0) * 10;

            return View(db.LFProduct.Where(a => a.ProductMark.Contains(pattern)).OrderBy(a => a.CategoryId).Skip(skipcount).Take(10).ToList());
        }

         private void SetDefaultInfo(LFProduct LFProduct)
        {
            ProductManager productmanager = new ProductManager();
            IList<Category> CategoryList = productmanager.getCategory(0);
            ViewData["categorylist"] = Utility.L1CategoryList;

            ViewData["secondcategorylist"] = Utility.L2CategoryList;

            IList<Brand> BrandList = productmanager.getBrand();
            ViewData["brandlist"] = (from s in BrandList
                                     select new SelectListItem
                                     {
                                         Selected = (s.BrandId == LFProduct.BrandId),
                                         Text = s.BrandName,
                                         Value = s.BrandId.ToString()
                                     }).ToList();
        }

    }
}
