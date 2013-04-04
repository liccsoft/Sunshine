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

        [Display(Name="产品名称")]
        public string ProductName { get; set; }
        [Display(Name = "型号")]
        public string ProductMark { get; set; }
        [Display(Name = "品牌")]
        public string ProductBrand { get; set; }
        [Display(Name = "尺寸")]
        public string ProductSize { get; set; }
        [Display(Name = "CPU")]
        public string ProductProcessor { get; set; }
        [Display(Name = "显卡")]
        public string ProductVideo { get; set; }
        [Display(Name = "价格区间")]
        public int? ProductPriceTypeId { get; set; }
        [ForeignKey("ProductPriceTypeId")]
        public virtual ProductPriceType ProductPriceType { get; set; }

        [Display(Name = "产品类型")]
        public int ProductTypeId { get; set; }
        [Display(Name = "所属分类")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "描述")]
        public string ProductDescription { get; set; }
    }
}
