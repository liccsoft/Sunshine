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
    public class AccountManager : IAccountManager, ISecurityGroupManager
    {
        public static IAccountManager Current = new AccountManager();
        UsersContext ctx = new UsersContext();
        public string GetNickName(int id)
        {

            {
                var user = ctx.Users.Find(id);
                return user == null ? null : user.NickName;
            }
        }

        public Core.UserProfile GetUserProfile(int id)
        {

            {
                return ctx.UserProfiles.Find(id);
            }
        }

        public void AddNewProfile(Core.UserProfile user)
        {
            {
                ctx.UserProfiles.Add(user);
                ctx.SaveChanges();
            }
        }

        public User AddNewUser(string name, bool isAdmin)
        {
            var user = ctx.Users.Add(new User { UserName = name, IsAdmin = isAdmin });
            ctx.SaveChanges();
            return user;
        }

        public void UpdateProfile(Core.UserProfile user)
        {

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

            {
                return ctx.Users.Take(100).ToList();
            }
        }


        public SecurityGroup CreateSecurityGroup(string groupName, string groupDesc, params string[] permissions)
        {
            List<RoleSecurityGroup> allRoles;
            {
                allRoles = ctx.Database.SqlQuery<RoleSecurityGroup>("select 0 as RoleSecurityGroupId, 0 as SecurityGroupId, RoleName, cast(0 as bit) IsEnabled from webpages_Roles").ToList();
            }
            SecurityGroup securityGroup = new SecurityGroup { SecurityGroupName = groupName, Description = groupDesc, RolesInGroup = allRoles };
            securityGroup.Permissions = permissions;

            return CreateSecurityGroup(securityGroup);
        }

        public SecurityGroup CreateSecurityGroup(SecurityGroup securityGroup)
        {

            {
                var result = ctx.SecurityGroups.Add(securityGroup);
                ctx.SaveChanges();
                //foreach (var item in securityGroup.Roles)
                //{
                //    item.SecurityGroupId = result.SecurityGroupId;
                //    db.RoleSecurityGroups.Add(item);
                //}
                //db.SaveChanges();
                return result;
            }

        }

        public SecurityGroup UpdateSecurityGroup(SecurityGroup securityGroup)
        {

                UpdateSecurityGroup(securityGroup, ctx);
            return securityGroup;
        }

        private SecurityGroup UpdateSecurityGroup(SecurityGroup securityGroup, UsersContext db)
        {
            db.Entry(securityGroup).State = System.Data.EntityState.Modified;
            ctx.SaveChanges();
            return securityGroup;
        }

        public SecurityGroup UpdateSecurityGroup(string name, params string[] permissions)
        {
                var item = GetSecurityGroup(name, ctx);
                item.Permissions = permissions;
                return UpdateSecurityGroup(item, ctx);
        }

        public void DeleteSecurityGroup(string name)
        {
            throw new NotImplementedException();
        }

        public void AddUserToGroup(string groupName, string userName)
        {
           var sg = GetSecurityGroup(groupName);
           if (sg != null)
           {
               var r = Roles.GetRolesForUser();
               if (r != null && r.Length > 0)
               {
                   Roles.RemoveUserFromRoles(userName, r);
               }

               Roles.AddUserToRoles(userName, sg.Permissions);
           }
        }

        public void AddUsersToGroup(string groupName, params string[] userNames)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserToGroup(string groupName, string userName)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersToGroup(string groupName, params string[] userNames)
        {
            throw new NotImplementedException();
        }

        private void Analyze(string[] p, string[] permissions, out List<string> added, out List<string> removed)
        {
            throw new NotImplementedException();
        }

        public SecurityGroup GetSecurityGroup(string name)
        {

            {
                return GetSecurityGroup(name, ctx);
            }
        }

        private static SecurityGroup GetSecurityGroup(string name, UsersContext ctx)
        {
            return ctx.SecurityGroups.Where(a => a.SecurityGroupName == name).FirstOrDefault();
        }

        public SecurityGroup GetSecurityGroup(int id)
        {
                var item = ctx.SecurityGroups.Find(id);
                if (item != null)
                {
                    item.RolesInGroup = ctx.RoleSecurityGroups.Where(a => a.SecurityGroupId == item.SecurityGroupId).ToList();
                }
                return item;
        }
        public List<SecurityGroup> GetAllSecurityGroup()
        {

            {
                return ctx.SecurityGroups.ToList();
            }
        }


        public void AddPermissionToGroup(string name, string permissions)
        {

            {
                ctx.RoleSecurityGroups.Add(new RoleSecurityGroup { });
            }
        }
        private void AddPermissionToGroup(int groupId, string permission, UsersContext ctx = null)
        {
            if (ctx == null)
                using (ctx = new UsersContext())
                {
                    ctx.RoleSecurityGroups.Add(new RoleSecurityGroup { SecurityGroupId = groupId, RoleName = permission });
                    ctx.SaveChanges();
                }
            else
            {
                ctx.RoleSecurityGroups.Add(new RoleSecurityGroup { SecurityGroupId = groupId, RoleName = permission });
            }
        }


        public SecurityGroup GetSecurityGroupById(int p)
        {
            if (p == 0) return SecurityGroup.Empty;


            {
                return ctx.SecurityGroups.Find(p);
            }
        }
    }
}
