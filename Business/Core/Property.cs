using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("Property")]
    public class Property
    {
        [Display(Name = "产品属性ID")]
        public string PropertyId { get; set; }
        [Display(Name = "产品属性名")]
        public string PropertyName { get; set; }
    }
}
