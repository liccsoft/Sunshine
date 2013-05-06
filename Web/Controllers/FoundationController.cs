using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Sunshine.Filters;
using Sunshine.Models;
using Sunshine.Business.Core;
using System.Data;
using System.Data.SqlClient;

namespace Sunshine.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class FoundationController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Foundation/

        public FoundationController()
        {
            ViewBag.ModuleName = "基础数据";
        }

        public ActionResult Index()
        {
            var color = db.ProductColors.ToDictionary<ProductColor, long>((a) => { return a.ProductColorId; });
            color.Add(0, new ProductColor() { ProductColorName = "无" });
            ViewData["Product"] = color;

            var size = db.ProductSizes.ToDictionary<ProductSize, long>((a) => { return a.ProductSizeId; });
            size.Add(0, new ProductSize() { ProductSizeName = "无" });
            ViewData["Size"] = size;

            var brand = db.Brands.ToDictionary<Brand, long>((a) => { return a.BrandId; });
            brand.Add(0, new Brand() { BrandName = "无" });
            ViewData["Brand"] = brand;

            var interval = db.PriceIntervals.ToDictionary<PriceInterval, long>((a) => { return a.PriceIntervalId; });
            interval.Add(0, new PriceInterval() { PriceIntervalName = "无" });
            ViewData["Interval"] = interval;

            return View(db.ProductColors.ToList());
        }

        //颜色================================
        //

        #region
        // GET: /Foundation/Create

        public ActionResult ColorCreate()
        {
            return View();
        }

        //
        // GET: /Foundation/Create

        [HttpPost]
        public ActionResult ColorCreate(ProductColor color)
        {
            if (ModelState.IsValid)
            {
                db.ProductColors.Add(color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // POST: /Foundation/Edit/5

        public ActionResult ColorEdit(int id = 0)
        {
            try
            {
                ProductColor color = db.ProductColors.Find(id);
                if (color == null)
                {
                    return HttpNotFound();
                }
                return View(color);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ColorEdit(ProductColor color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(color);
        }

        //
        // GET: /Foundation/Delete/5

        public ActionResult ColorDelete(int id = 0)
        {
            ProductColor color = db.ProductColors.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        //
        // POST: /Foundation/Delete/5

        [HttpPost, ActionName("ColorDelete")]
        public ActionResult ColorDeleteConfirmed(int id)
        {
            ProductColor color = db.ProductColors.Find(id);
            db.ProductColors.Remove(color);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        //尺寸==============================
        //

        #region
        [ChildActionOnly]
        //[OutputCache(Duration = 100000)]
        public ActionResult SizeManager()
        {
            var size = db.ProductSizes.ToDictionary<ProductSize, long>((a) => { return a.ProductSizeId; });
            size.Add(0, new ProductSize() { ProductSizeName = "无" });
            ViewData["Size"] = size;
            return View(db.ProductSizes.ToList());
        }

        public ActionResult SizeCreate()
        {
            return View();
        }

        //
        // GET: /Foundation/Create

        [HttpPost]
        public ActionResult SizeCreate(ProductSize size)
        {
            if (ModelState.IsValid)
            {
                db.ProductSizes.Add(size);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("SizeManager");
        }

        //
        // POST: /Foundation/Edit/5

        public ActionResult SizeEdit(int id = 0)
        {
            try
            {
                ProductSize size = db.ProductSizes.Find(id);
                if (size == null)
                {
                    return HttpNotFound();
                }
                return View(size);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SizeEdit(ProductSize size)
        {
            if (ModelState.IsValid)
            {
                db.Entry(size).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("SizeManager");
        }

        //
        // GET: /Foundation/Delete/5

        public ActionResult SizeDelete(int id = 0)
        {
            ProductSize size = db.ProductSizes.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        //
        // POST: /Foundation/Delete/5

        [HttpPost, ActionName("SizeDelete")]
        public ActionResult SizeDeleteConfirmed(int id)
        {
            ProductSize size = db.ProductSizes.Find(id);
            db.ProductSizes.Remove(size);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        //价格区间==============================
        //

        #region
        [ChildActionOnly]
        //[OutputCache(Duration = 100000)]
        public ActionResult PriceIntervalManager()
        {
            return View(db.PriceIntervals.ToList());
        }

        public ActionResult PriceIntervalCreate()
        {
            return View();
        }

        //
        // GET: /Foundation/Create

        [HttpPost]
        public ActionResult PriceIntervalCreate(PriceInterval interval)
        {
            if (ModelState.IsValid)
            {
                db.PriceIntervals.Add(interval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("PriceIntervalManager");
        }

        //
        // POST: /Foundation/Edit/5

        public ActionResult PriceIntervalEdit(int id = 0)
        {
            try
            {
                PriceInterval interval = db.PriceIntervals.Find(id);
                if (interval == null)
                {
                    return HttpNotFound();
                }
                return View(interval);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult PriceIntervalEdit(PriceInterval interval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("PriceIntervalManager");
        }

        //
        // GET: /Foundation/Delete/5

        public ActionResult PriceIntervalDelete(int id = 0)
        {
            PriceInterval interval = db.PriceIntervals.Find(id);
            if (interval == null)
            {
                return HttpNotFound();
            }
            return View(interval);
        }

        //
        // POST: /Foundation/Delete/5

        [HttpPost, ActionName("PriceIntervalDelete")]
        public ActionResult PriceIntervalDeleteConfirmed(int id)
        {
            PriceInterval interval = db.PriceIntervals.Find(id);
            db.PriceIntervals.Remove(interval);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        //品牌==============================
        //

        #region
        [ChildActionOnly]
        //[OutputCache(Duration = 100000)]
        public ActionResult BrandManager()
        {
            return View(db.Brands.ToList());
        }

        public ActionResult BrandCreate()
        {
            return View();
        }

        //
        // GET: /Foundation/Create

        [HttpPost]
        public ActionResult BrandCreate(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("BrandManager");
        }

        //[HttpPost]
        //public JsonResult BrandCreate(Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Brands.Add(brand);
        //        db.SaveChanges();
        //        return Json(new { message = "公司信息不可修改", status = false });
        //    }
        //}

        //
        // POST: /Foundation/Edit/5

        public ActionResult BrandEdit(int id = 0)
        {
            try
            {
                Brand brand = db.Brands.Find(id);
                if (brand == null)
                {
                    return HttpNotFound();
                }
                return View(brand);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult BrandEdit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("BrandManager");
        }

        //
        // GET: /Foundation/Delete/5

        public ActionResult BrandDelete(int id = 0)
        {
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        //
        // POST: /Foundation/Delete/5

        [HttpPost, ActionName("BrandDelete")]
        public ActionResult BrandDeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
