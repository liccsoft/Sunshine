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
        public int UserProfileId { get; set; }

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
        [DataType(DataType.EmailAddress)]
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
        [DataType(DataType.Url)]
        [Display(Name = "微博", GroupName = "基本信息")]
        public string Weibo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "地址", GroupName = "基本信息")]
        public string Address { get; set; }

        [MaxLength(32)]
        [Display(Name = "电话", GroupName = "基本信息")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        [MaxLength(32)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "手机", GroupName = "基本信息")]
        public string Mobile { get; set; }
    }


    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContactId { get; set; }

        public string Email { get; set; }
    
    }

}
