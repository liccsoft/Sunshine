using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Account
{
    [Table("SecurityGroup")]
    public class SecurityGroup
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SecurityGroupId { get; set; }
        [Display(Name = "安全组名称")]
        public string SecurityGroupName { get; set; }
        [Display(Name = "安全组说明")]
        public string Description { get; set; }

        [ForeignKey("SecurityGroupId")]
        public virtual List<RoleSecurityGroup> Roles { get; set; }

        public string[] Permissions { get {
            return Roles == null ? new string[0] : Roles.ConvertAll(a => a.RoleName).ToArray();
        }
            set
            {
                List<RoleSecurityGroup> list = new List<RoleSecurityGroup>();
                foreach (var r in value)
                {
                     RoleSecurityGroup item;
                    if (Roles != null && (item=Roles.Find(a=> a.RoleName == r)) != null)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        list.Add(new RoleSecurityGroup { RoleName = r, SecurityGroupId = SecurityGroupId });
                    }
                }

                if (Roles != null)
                {
                    Roles.Clear();
                    Roles.AddRange(list);
                }
                else
                Roles = list;
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
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int RoleSecurityGroupId { get; set; }
        public int SecurityGroupId { get; set; }
        [MaxLength(128)]
        public string RoleName { get; set; }
    }
}
