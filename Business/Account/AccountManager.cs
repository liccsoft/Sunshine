using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;
using System.Data.SqlClient;
using Sunshine.Business.Security;
using System.Web.Security;

namespace Sunshine.Business.Account
{
    public class AccountManager : IAccountManager
    {
        public static IAccountManager Current = new AccountManager();
        public string GetNickName(int id)
        {
            using (var ctx = new UsersContext())
            {
                var user = ctx.Users.Find(id);
                return user == null ? null : "";
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

        public User AddNewUser(string name, bool isAdmin)
        {
            using (var ctx = new UsersContext())
            {
                var user = ctx.Users.Add(new UserInternal { UserName = name, IsAdmin = isAdmin });
                ctx.SaveChanges();
                return new User(user);
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

        public List<User> GetUsers(bool isAdmin, int sindex, int eindex)
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Users.Where(a=>a.IsAdmin == isAdmin).OrderBy(a=>a.UserId).Skip(sindex).Take(eindex-sindex).ToList().ConvertAll<User>(a=>new User(a));
            }
        }

        public SecurityGroup GetSecurityGroupById(int p)
        {
            if (p == 0) return SecurityGroup.Empty;

            using(var ctx = new UsersContext())
            {
                return ctx.SecurityGroups.Find(p);
            }
        }

        public User GetUser(string p)
        {
            using (var ctx = new UsersContext())
            return new User(ctx.Users.Where(a => a.UserName == p).FirstOrDefault());
        }

        public List<User> GetAdminUsers(int sindex, int eindex)
        {
            return GetUsers(true, sindex, eindex);
        }

        public List<User> GetNormalUsers(int sindex, int eindex)
        {
            return GetUsers(false, sindex, eindex);
        }
    }
}
