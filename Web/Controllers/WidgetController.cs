using Sunshine.Business.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunshine.Controllers
{
    public class WidgetController : Controller
    {
        //
        // GET: /Widget/

        public ActionResult Company(string name, int? pageIndex)
        {
            using (var db = new UsersContext())
            {
                return View(db.Companys.Where(a=>a.CompanyName.Contains(name)).Take(10).ToList());
            }
            
        }

    }
}
