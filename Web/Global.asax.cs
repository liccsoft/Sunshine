﻿using System;
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
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            Database.SetInitializer<UsersContext>(null);

            try
            {
                using (var context = new UsersContext())
                {
                    if (!context.Database.Exists())
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }
                }

                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "User", "UserId", "UserName", autoCreateTables: true);

                if (!WebSecurity.UserExists("admin"))
                {
                    WebSecurity.CreateUserAndAccount("admin", "password");
                }

                if (!Roles.RoleExists("admin"))
                {
                    Roles.CreateRole("admin");
                    Roles.AddUserToRole("admin", "admin");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }

        }

        protected void Application_AuthenticateRequest()
        {
            //if (WebSecurity.IsAuthenticated && WebSecurity.CurrentUserId < 0)
            //{
            //    WebSecurity.Logout();
            //}
        }
    }
}