using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;

namespace Sunshine.Controllers
{
    public class CompanyController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Company/

        public ActionResult Index()
        {
            var companys = db.Companys.Include(c => c.CompanyTraderKind);
            return View(companys.ToList());
        }

        //
        // GET: /Company/Details/5

        public ActionResult BusinessDetail(string name, int id = 0)
        {
            Company company = null;
            if (id > 0)
            {
                company = db.Companys.Find(id);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                company = db.Companys.FirstOrDefault(a => a.CompanyName == name);
            }

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            ViewBag.CompanyTraderKindId = new SelectList(db.TraderKinds, "TraderKindId", "TraderKindName");
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companys.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyTraderKindId = new SelectList(db.TraderKinds, "TraderKindId", "TraderKindName", company.CompanyTraderKindId);
            return View(company);
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Company company = db.Companys.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyTraderKindId = new SelectList(db.TraderKinds, "TraderKindId", "TraderKindName", company.CompanyTraderKindId);
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyTraderKindId = new SelectList(db.TraderKinds, "TraderKindId", "TraderKindName", company.CompanyTraderKindId);
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Company company = db.Companys.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companys.Find(id);
            db.Companys.Remove(company);
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