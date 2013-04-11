using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.Business.Core
{
    [Table("PriceInterval")]
    public class PriceInterval
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int PriceIntervalId { get; set; }

        [Display(Name = "价格区间")]
        public string PriceIntervalName { get; set; }
    }
}
