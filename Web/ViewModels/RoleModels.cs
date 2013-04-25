using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
        [JsonProperty("rn")]
        public string RoleName { get; set; }
        [Display(Name = "用户名")]
        [JsonProperty("un")]
        public string UserName { get; set; }
        [Display(Name = "用户ID")]
        [JsonProperty("uid")]
        public int UserId { get; set; }
    }
}