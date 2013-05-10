using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Sunshine.Business.Core
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(128, MinimumLength=6, ErrorMessage="请输入大于6个字符")]
        [Display(Name="用户名")]
        public string UserName { get; set; }

        [Display(Name = "用户昵称")]
        public string NickName { get { return Profile == null ? null : Profile.NickName; } }

        public int? UserProfileId { get; set; }

        [ForeignKey("UserProfileId")]
        public virtual UserProfile Profile { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [Display(Name = "所属公司")]
        public virtual Company Company { get; set; }

        [DefaultValue(0)]
        public int? ProfileStatus { get; set; }
        [DefaultValue(0)]
        public int? CompanyStatus { get; set; }

       // [EnumDataType(typeof(int))]
        public ModifyStatus ProfileModifyStatus
        {
            get
            {
                return (ModifyStatus) (ProfileStatus ?? 0);
            }
            set
            {
                ProfileStatus = (int)value;
            }
        }

        //[EnumDataType(typeof(int))]
        public ModifyStatus CompanyModifyStatus
        {
            get
            {
                return (ModifyStatus)(CompanyStatus??0);
            }
            set
            {
                CompanyStatus = (int)value;
            }
        }
        /// <summary>
        /// 0 normal, 1: admin
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
