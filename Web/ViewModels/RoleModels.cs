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
}