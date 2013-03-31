using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Account
{
    public class AccountManager : IAccountManager
    {
        public static IAccountManager Current = new AccountManager();
        public string GetNickName(int id)
        {
            using (var ctx = new UsersContext())
            {
                return ctx.UserProfiles.Find(id).NickName;
            }
        }

        public Core.UserProfile GetUserProfile(int id)
        {
            using (var ctx = new UsersContext())
            {
                return ctx.UserProfiles.Find(id);
            }
        }

        public void AddNewProfile(Core.UserProfile user)
        {
            using (var ctx = new UsersContext())
            {
                ctx.UserProfiles.Add(user);
                ctx.SaveChanges();
            }
        }

        public void UpdateProfile(Core.UserProfile user)
        {
            using (var ctx = new UsersContext())
            {
                ctx.Entry(user).State = System.Data.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeactiveUser(int userId)
        {
           
        }
    }
}
