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

        public ActionResult Company()
        {
            using (var db = new UsersContext())
            {
                return View(db.Companys.ToList());
            }
            
        }

    }
}
