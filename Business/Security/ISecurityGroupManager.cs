using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Account;

namespace Sunshine.Business.Security
{
    public interface ISecurityGroupManager
    {
        SecurityGroup CreateSecurityGroup(string name, string groupDesc, params string[] permissions);
        SecurityGroup UpdateSecurityGroup(string name, params string[] permissions);
        SecurityGroup UpdateSecurityGroup(SecurityGroup group);
        SecurityGroup GetSecurityGroup(string name);
        List<SecurityGroup> GetAllSecurityGroup();
        void AddPermissionToGroup(string name, string permissions);
        void DeleteSecurityGroup(string name);
        void AddUserToGroup(string groupName, string userName);
        void AddUsersToGroup(string groupName, params string[] userNames);
        void RemoveUserToGroup(string groupName, string userName);
        void RemoveUsersToGroup(string groupName, params string[] userNames);

        SecurityGroup GetSecurityGroupById(int p);
    }
}
