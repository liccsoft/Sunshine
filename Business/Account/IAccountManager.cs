using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Account
{
    public interface IAccountManager
    {
        UserProfile GetUserProfile(int id);
        void AddNewProfile(UserProfile user);
        void UpdateProfile(UserProfile user);
        void DeactiveUser(int userId);

        List<User> GetUsers(bool isAdmin, int sindex, int eindex);
        List<User> GetAdminUsers(int sindex, int eindex);
        List<User> GetNormalUsers(int sindex, int eindex);
        User AddNewUser(string name, bool isAdmin);

        User GetUser(string p);
    }
}
