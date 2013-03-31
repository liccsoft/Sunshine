using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sunshine.Business.Account;
using WebMatrix.WebData;

namespace Sunshine
{
    public static class Utility
    {
        public static string CurrentNickName
        {
            get
            {
                if (HttpContext.Current.Items["CurrentUserNickName"] == null)
                {
                    HttpContext.Current.Items["CurrentUserNickName"] = new AccountManager().GetNickName(WebSecurity.CurrentUserId);
                }

                return HttpContext.Current.Items["CurrentUserNickName"] as string;
            }
        }
    }
}