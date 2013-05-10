using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Sunshine.Business.Security
{
    public class MyRoleProvider
    {
        public  bool IsUserInRole(string username, string roleName)
        {
            return false;
        }
    }
}
