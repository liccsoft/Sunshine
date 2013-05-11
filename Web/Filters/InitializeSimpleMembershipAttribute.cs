using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using Sunshine.Business.Core;
using WebMatrix.WebData;
using Sunshine.Business.Account;
using System.Web.Security;

namespace Sunshine.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {                            
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();

                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "User", "UserId", "UserName", autoCreateTables: true);

                    if (!WebSecurity.UserExists("superadmin"))
                    {
                        AccountManager.Current.AddNewUser("superadmin", true);
                        WebSecurity.CreateAccount("superadmin", "password");
                        foreach (var name in new string[] { "Security", "Company", "Category", "Account", "Manage" })
                        {
                            if (!Roles.RoleExists(name))
                            {
                                Roles.CreateRole(name);
                                Roles.AddUserToRole("superadmin", name);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
