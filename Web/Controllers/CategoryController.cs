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
    public class CategoryController : Controller
    {
        private UsersContext db = new UsersContext();
        public CategoryController()
        {
            ViewBag.ModuleName = "产品分类管理";
        }
        //
        // GET: /Category/

        public ActionResult Index()
        {
            var category = db.Categorys.ToDictionary<Category, int>((a) => { return a.CategoryId; });
            ViewData["Category"] = category;
            return View(db.Categorys.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Status = EntityStatus.Enabled;
                if (category.ParentCategoryId == 0)
                {
                    category.ParentCategoryId = null;
                }
                else
                {
                    var p = db.Categorys.Find(category.ParentCategoryId);
                    category.CategoryLevel = (p==null)? 0: p.CategoryLevel + 1;
                }
                db.Categorys.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var orginal = db.Categorys.Find(category.CategoryId);
                orginal.Description = category.Description;
                orginal.Title = category.Title;
                db.Entry(orginal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            if (category.CategoryLevel == 0)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categorys.Find(id);
            if (category.CategoryLevel != 0)
            {
                db.Categorys.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}