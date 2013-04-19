using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.ViewModels
{
    public class RoleModel
    {
        [Display(Name="角色名")]
        public string RoleName { get; set; }
    }

    public class UserRoleModel
    {
        [Display(Name = "角色名")]
        public string RoleName { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "用户ID")]
        public int UserId { get; set; }
        [Display(Name = "用户昵称")]
        public string NickName { get; set; }
        [Display(Name = "所属公司")]
        public string CompanyName { get; set; }
    }
}