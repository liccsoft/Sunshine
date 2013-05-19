using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using Sunshine.Business.Core;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Security;
using Sunshine.Business.Account;
using Sunshine.ViewModels;
using System.IO;
using System.Xml.Serialization;

namespace Sunshine
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModuleManager manager;
            using (var stream = new FileStream(HttpContext.Current.Server.MapPath("~/Modules.xml"), FileMode.OpenOrCreate))
            {
                var serialzie = new XmlSerializerFactory().CreateSerializer(typeof(ModuleManager));
                manager = serialzie.Deserialize(stream) as ModuleManager;
                if (manager != null)
                {
                    HttpContext.Current.Cache["ModuleManager"] = manager;
                }
            }
        }

        protected void Application_AuthenticateRequest()
        {
        }
    }
}