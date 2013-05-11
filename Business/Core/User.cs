using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Sunshine.Business.Account;
using System.Data;
using System.Data.SqlClient;

namespace Sunshine.Business.Core
{

    [Table("User")]
    public class UserInternal
    {
        public UserInternal()
        {
            CreationTime = DateTime.Now;
            ProfileStatus = (int) ModifyStatus.None;
            CompanyStatus = (int) ModifyStatus.None;
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 6, ErrorMessage = "请输入大于6个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        public int? UserProfileId { get; set; }

        public int? CompanyId { get; set; }

        [DefaultValue(0)]
        public int? ProfileStatus { get; set; }

        [DefaultValue(0)]
        public int? CompanyStatus { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime CreationTime { get; set; }
    }

    public class User
    {
        private UserInternal userInternal;
        private UserProfile profile;
        private Company company;

        public User(int userId = 0)
        {
            if (userId == 0)
            {
                userInternal = new UserInternal();
            }
            else
            {
                using (var ctx = new UsersContext())
                {
                    userInternal = ctx.Users.Find(userId);
                }
                if (userInternal == null) throw new ObjectNotFoundException("User");
            }
        }

        internal User(UserInternal user)
        {
            this.userInternal = user;
        }
        [Required]
        [StringLength(128, MinimumLength=6, ErrorMessage="请输入大于6个字符")]
        [Display(Name = "用户账号")]
        public string UserName { get { return userInternal.UserName; } }

        [Display(Name = "用户昵称")]
        public string NickName { get { return Profile == null ? null : Profile.NickName; } }

        public UserProfile Profile
        {
            get
            {
                if (profile == null)
                {
                    LoadProfile();
                }
                return profile;
            }
        }

        [Display(Name = "所属公司")]
        public Company Company
        {
            get
            {
                if (company == null)
                {
                    LoadCompany();
                }
                return company;
            }
        }

        public ModifyStatus ProfileModifyStatus
        {
            get
            {
                return (ModifyStatus)(userInternal.ProfileStatus ?? 0);
            }
            set
            {
                userInternal.ProfileStatus = (int)value;
            }
        }

        public ModifyStatus CompanyModifyStatus
        {
            get
            {
                return (ModifyStatus)(userInternal.CompanyStatus ?? 0);
            }
            set
            {
                userInternal.CompanyStatus = (int)value;
            }
        }

        public bool IsAdmin { get { return userInternal.IsAdmin; } }

        public int UserId { get { return userInternal.UserId; } }

        private void LoadProfile()
        {
            if(userInternal.UserProfileId!=null)
            using (var ctx = new UsersContext())
            {
                profile = ctx.UserProfiles.Find(userInternal.UserProfileId);
            }
        }
        private void LoadCompany()
        {
            if (userInternal.CompanyStatus != (int)ModifyStatus.None)
            using (var ctx = new UsersContext())
            {
                company = ctx.Companys.Find(userInternal.CompanyId);
            }
        }

        public void UpdateProfile(UserProfile profile)
        {
            if (userInternal.UserProfileId == profile.UserProfileId)
            {
                using (var ctx = new UsersContext())
                {
                    var temp = ctx.Entry<UserProfile>(profile);
                    temp.State = EntityState.Modified; ;
                    ctx.SaveChanges();
                    this.profile = temp.Entity;
                }
            }
            else if ((userInternal.UserProfileId ?? 0) != 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                using (var ctx = new UsersContext())
                {
                    profile = ctx.UserProfiles.Add(profile);
                    ctx.SaveChanges();
                    userInternal.UserProfileId = profile.UserProfileId;
                    ctx.Entry(userInternal).State = EntityState.Modified;
                    ctx.SaveChanges();
                    this.profile = profile;
                }
            }
        }
    }
}
