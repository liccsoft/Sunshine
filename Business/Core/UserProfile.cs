using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /// <summary>
        /// 用户名，登陆唯一标识符
        /// </summary>
        [MaxLength(128)]
        [Required]
        [Display(Name="用户名", GroupName="基本信息")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称，显示
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "昵称", GroupName = "基本信息")]
        public string NickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        [RegularExpression(".*@.*",ErrorMessage="邮箱格式错误")]
        [Display(Name = "邮箱", GroupName = "基本信息")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        public string QQ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        public string Weibo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "地址", GroupName = "基本信息")]
        public string Address { get; set; }

        [MaxLength(32)]
        [Display(Name = "电话", GroupName = "基本信息")]
        public string Tel { get; set; }

        [MaxLength(32)]
        public string Mobile { get; set; }
    }
}
