using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string UserName { get; set; }
         [MaxLength(128)]
        [DataType(DataType.EmailAddress)]
        public string SecurityEmail { get; set; }

        public string NickName { get { return Profile == null ? null : Profile.NickName; } }

        public int? UserProfileId { get; set; }

        [ForeignKey("UserProfileId")]
        public virtual UserProfile Profile { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public int? ProfileStatus { get; set; }
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
    }
}
