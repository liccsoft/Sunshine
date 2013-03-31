using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Account
{
    public interface IAccountManager
    {
        string GetNickName(int id);
        UserProfile GetUserProfile(int id);
        void AddNewProfile(UserProfile user);
        void UpdateProfile(UserProfile user);
        void DeactiveUser(int userId);
    }
}
