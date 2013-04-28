using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;
using System.Data.SqlClient;

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
                return user==null ? null: user.NickName;
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


        public List<User> GetUsers()
        {
            using (var ctx = new UsersContext())
            {
                return ctx.Users.Take(100).ToList();
            }
        }


        public SecurityGroup CreateSecurityGroup(string groupName, string groupDesc, params string[] permissions)
        {
            SecurityGroup securityGroup = new SecurityGroup { SecurityGroupName = groupName, Description = groupDesc, Permissions = permissions };

            return CreateSecurityGroup(securityGroup);
        }

        public SecurityGroup CreateSecurityGroup(SecurityGroup securityGroup)
        {
            using (var db = new UsersContext())
            {
                if (securityGroup.Roles == null)
                    securityGroup.Roles = new List<RoleSecurityGroup>();
                securityGroup = db.SecurityGroups.Add(securityGroup);
                db.SaveChanges();
                //foreach(var role in securityGroup.Permissions)
                //{
                //    securityGroup.Roles.Add(new RoleSecurityGroup() { SecurityGroupId = securityGroup.SecurityGroupId, RoleName = role });
                //}
                //db.Entry(securityGroup).State = System.Data.EntityState.Modified;
                //db.SaveChanges();
            }
            return securityGroup;
        }

        public SecurityGroup UpdateSecurityGroup(SecurityGroup securityGroup)
        {
            using (var db = new UsersContext())
            {
                var orignal = db.RoleSecurityGroups.Where(a=> a.SecurityGroupId == securityGroup.SecurityGroupId).ToList();

                if (securityGroup.Roles!=null)
                foreach (var item in securityGroup.Roles)
                {
                    if (!orignal.Exists(a => a.RoleName == item.RoleName))
                    {
                        db.RoleSecurityGroups.Add(item);
                    }
                  // db.RoleSecurityGroups
                }
                foreach (var item in orignal)
                {
                    if (securityGroup.Roles == null || !securityGroup.Roles.Exists(a=> a.RoleName == item.RoleName))
                    {
                        db.Entry(item).State = System.Data.EntityState.Deleted;
                    }
                }
                db.SaveChanges();
            }
            return securityGroup;
        }
    }
}
