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
    public class AccountController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Account/Login

        public AccountController()
        {
            ViewBag.ModuleName = "个人中心";
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {                    
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }
            
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

       
        //
        // GET: /Account/Manage
        [ChildActionOnly]
        [OutputCache( Duration = 100000)]
        public ActionResult pwd()
        {
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pwd(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(UserProfile profile)
        {
            try
            {
                if (profile.UserProfileId > 0)
                {
                    var temp = db.Entry<UserProfile>(profile);
                    temp.State = EntityState.Modified; ;
                    db.SaveChanges();
                    ViewBag.Message = "修改成功";
                    profile = temp.Entity;
                }
                else
                {
                    profile = db.UserProfiles.Add(profile);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("update [User] set UserProfileId = @id where [userid]=@uid", new SqlParameter("id", profile.UserProfileId), new SqlParameter("uid", WebSecurity.CurrentUserId));
                }
            }
            catch
            {
                ViewBag.Message = "修改失败";
            }

            return View(db.Users.Find(WebSecurity.CurrentUserId));
        }
        User currentUser;
        public ActionResult Manage()
        {
            currentUser = db.Users.Find(WebSecurity.CurrentUserId);
            return View(currentUser);
        }

        [HttpPost]
        public JsonResult EditDetail(UserProfile user)
        {
            if (user.UserProfileId == WebSecurity.CurrentUserId)
            {
                db.Entry<UserProfile>(user).State = EntityState.Modified; ;
                db.SaveChanges();
            }

            return Json(new {data = user, message="修改成功", status = true});
        }

        [HttpPost]
        public JsonResult EditCompany(Company company)
        {
            var user = db.Users.Find(WebSecurity.CurrentUserId);

            if (company.CompanyId > 0 && company.CreatedUserName != user.UserName)
            {
                db.Database.ExecuteSqlCommand("update [User] set CompanyId = @id where [userid]=@uid", new SqlParameter("id", company.CompanyId), new SqlParameter("uid", user.UserId));
           
                return Json(new { message = "修改成功", status = true });
            }

            if (user.CompanyId != company.CompanyId && company.CompanyId > 0)
            {
                return Json(new { message = "修改失败", status = false });
            }

            if (company.CompanyId > 0)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                company.CreatedUserName = user.UserName;
               var c = db.Companys.Add(company);
               db.SaveChanges();
               db.Database.ExecuteSqlCommand("update [User] set CompanyId = @id where [userid]=@uid", new SqlParameter("id", c.CompanyId), new SqlParameter("uid", user.UserId));
            }

            return Json(new { message = "修改成功", status= true});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
