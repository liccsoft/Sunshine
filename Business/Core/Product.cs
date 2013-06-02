using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        // delete property
        //[Display(Name = "产品名称")]
        //public string ProductName { get; set; }

        //[Display(Name = "CPU")]
        //public string ProductProcessor { get; set; }
        //[Display(Name = "显卡")]
        //public string ProductVideo { get; set; }

        //
        //[Display(Name = "价格区间")]
        //public int? PriceIntervalId { get; set; }
        //[ForeignKey("PriceIntervalId")]
        //public virtual PriceInterval PriceInterval { get; set; }

        //[Display(Name = "尺寸")]
        //public int? ProductSizeId { get; set; }
        //[ForeignKey("ProductSizeId")]
        //public virtual ProductSize ProductSize { get; set; }

        //[Display(Name = "代理价")]
        //public decimal AgentPrice { get; set; }

        //[Display(Name = "颜色")]
        //public int ProductColorId { get; set; }
        //[ForeignKey("ProductColorId")]
        //public virtual ProductColor ProductColor { get; set; }

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

        [Display(Name = "提货价")]
        public string DeliveryPrice { get; set; }

        [Display(Name = "库存")]
        public int ProductStock { get; set; }

        [Display(Name = "录入人员")]
        public int UserId { get; set; }

        [Display(Name = "录入时间")]
        public DateTime Createtime { get; set; }

        [Display(Name = "更新时间")]
        public DateTime Updatetime { get; set; }
    }
}
