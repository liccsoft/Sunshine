using System;
using System.ComponentModel.DataAnnotations;
namespace Sunshine.Business.Core
{
    interface IBaseType
    {
        [Display(Name = "描述")]
        string Description { get; set; }
        [Display(Name = "显示名称")]
        string DisplayName { get; set; }
        [Display(Name = "名称")]
        string Name { get; set; }
    }
}
