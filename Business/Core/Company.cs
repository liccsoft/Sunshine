using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("Company")]
    public class Company
    {
        private List<User> _allMembers;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Display(Name = "商家类别")]
        public int? CompanyTraderKindId { get; set; }

        [Display(Name = "商家类别")]
        [ForeignKey("CompanyTraderKindId")]
        public TraderKind CompanyTraderKind { get; set; }

        [Display(Name = "付款方式")]
        [StringLength(4000)]
        public string Payment { get; set; }

        public bool IsOwner(User currentUser)
        {
            return currentUser.UserName == CreatedUserName;
        }

        
        public List<User> AllMembers()
        {
                LoadMembers();
                return _allMembers;
        }

        private void LoadMembers()
        {
            if (_allMembers == null)
            {
                using (var ctx = new UsersContext())
                {
                    _allMembers = ctx.Users.Where(a=>a.CompanyId == CompanyId).ToList().ConvertAll(a=>new User(a));
                }
            }
        }
    }

    /// <summary>
    /// 商家类型
    /// </summary>
    [Table("TradeKinds")]
    public class TraderKind
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TraderKindId { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name="商家类别")]
        public string TraderKindName { get; set; }

        [Display(Name = "商家类别描述")]
        public string Description { get; set; }
    }
}
