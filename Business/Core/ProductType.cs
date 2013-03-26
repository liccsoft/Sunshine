using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
     [Table("ProductType")]
    public class ProductType : Sunshine.Business.Core.IBaseType
    {
         [Key]
         [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
         public int ProductTypeId { get; set; }

         public string Name { get; set; }

         public string DisplayName { get; set; }

         public string Description { get; set; }
    }
}
