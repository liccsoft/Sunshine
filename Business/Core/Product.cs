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
        [Display(Name = "产品型号")]
        public string ProductMark { get; set; }
        [Display(Name = "产品描述")]
        public string ProductDescription { get; set; }
        [Display(Name = "价格区间")]
        public int ProductPriceTypeId { get; set; }
        [Display(Name = "产品类型")]
        public int ProductTypeId { get; set; }
        [Display(Name = "所属分类")]
        public int CategoryId { get; set; }
    }
}
