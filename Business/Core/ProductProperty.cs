using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("ProductProperty")]
    public class ProductProperty
    {
        [Display(Name = "产品ID")]
        public long ProductId { get; set; }
        [Display(Name = "产品属性ID")]
        public string PropertyId { get; set; }
        [Display(Name = "产品属性值")]
        public string PropertyValue { get; set; }
    }
}
