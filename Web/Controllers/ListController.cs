using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        //
        // POST: /Merchant/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchant/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Merchant/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Merchant/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
