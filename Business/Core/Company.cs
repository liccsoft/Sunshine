﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("Company")]
    public class Company
    {
        private List<User> _allMembers;
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }

        [StringLength(256)]
        [Required]
        [Display(Name="公司名称")]
        public string CompanyName { get; set; }
        [Display(Name = "公司地址")]
        public string Address { get; set; }

        [StringLength(15)]
        [Display(Name = "固定电话")]
        public string TelNumber { get; set; }
        [Display(Name = "移动电话")]
        public string Mobile { get; set; }
        [Display(Name = "公司描述")]
        public string Description { get; set; }
        [Display(Name = "创建者")]
        public string CreatedUserName { get; set; }

        public bool IsOwner(User CurrentUser)
        {
            return CurrentUser.UserName == CreatedUserName;
        }

        
        public List<User> AllMembers()
        {
                loadMembers();
                return _allMembers;
        }

        private void loadMembers()
        {
            if (_allMembers == null)
            {
                using (UsersContext ctx = new UsersContext())
                {
                    _allMembers = ctx.Users.Where(a=>a.CompanyId == CompanyId).ToList().ConvertAll<User>(a=>new User(a));
                }
            }
        }
    }
}
