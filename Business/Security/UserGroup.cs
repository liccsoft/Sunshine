using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace Sunshine.Business.Account
{
    [Table("SecurityGroup")]
    public class SecurityGroup
    {
        public static readonly SecurityGroup Empty;
        static SecurityGroup()
        {
            Empty = new SecurityGroup();
            Empty.RolesInGroup = Roles.GetAllRoles().ToList().ConvertAll<RoleSecurityGroup>(a => new RoleSecurityGroup { RoleName = a });
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SecurityGroupId { get; set; }
        [Display(Name = "安全组名称")]
        public string SecurityGroupName { get; set; }
        [Display(Name = "安全组说明")]
        public string Description { get; set; }

        [ForeignKey("SecurityGroupId")]
        public virtual List<RoleSecurityGroup> RolesInGroup { get; set; }

        public string[] Permissions
        {
            get
            {
                return RolesInGroup == null ? new string[0] : RolesInGroup.Where(a => a.IsEnabled).ToList().ConvertAll(a => a.RoleName).ToArray();
            }
            set
            {
                if(RolesInGroup != null)
                foreach (var item in RolesInGroup)
                {
                    item.IsEnabled = value.ToList().Any(a => a == item.RoleName);
                }
            }
        }

        public bool HasPermission(string role)
        {
            return Permissions!=null && Permissions.Any(a => a == role);
        }
    }

    [Table("RoleSecurityGroup")]
    public class RoleSecurityGroup
    {
        //[Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        //public int RoleSecurityGroupId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int SecurityGroupId { get; set; }
        [Key]
        [MaxLength(128)]
        [Column(Order = 2)]
        public string RoleName { get; set; }
        public bool IsEnabled { get; set; }
    }


}
