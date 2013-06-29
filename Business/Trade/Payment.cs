using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Sunshine.Business.Trade
{
    public class Payment
    {

        public int PaymentId { get; set; }
        public PaymentType PaymentType;
        public string PaymentDetail;
        
    }

    [Flags]
    public enum  PaymentType
    {
        [Display(Name = "现金支付")]
        Cash = 1,
        [Display(Name = "刷卡支付")]
        Pos = 2,
        [Display(Name = "银行转账")]
        Card = 4
    }
}
