using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Sunshine.Business.Core
{
    [Table("LFProduct")]
    public class LFProduct
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long LFProductId { get; set; }

        [Display(Name = "型号")]
        public string ProductMark { get; set; }

        [Display(Name = "品牌")]
        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [Display(Name = "一级分类")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category CategoryMain { get; set; }

        [Display(Name = "二级分类")]
        public int? SecondCategoryId { get; set; }
        [ForeignKey("SecondCategoryId")]
        public virtual Category CategorySecond { get; set; }

        [Display(Name = "备注")]
        public string ProductDescription { get; set; }

        [Display(Name = "配置")]
        public string ProductAdditions { get; set; }

        [Display(Name = "数量")]
        public string ProductNum { get; set; }

        [Display(Name = "录入人员")]
        public int UserId { get; set; }

        [Display(Name = "录入时间")]
        public DateTime Createtime { get; set; }

        [Display(Name = "更新时间")]
        public DateTime Updatetime { get; set; }

    }
}
