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

        public string ProductName { get; set; }

        public string ProductMark { get; set; }

        public string ProductDescription { get; set; }

        public int ProductPriceTypeId { get; set; }

        public int ProductTypeId { get; set; }

        public int CategoryId { get; set; }
    }
}
