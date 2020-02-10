using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.PaymentModel
{
    public class PaymentRefundViewModel
    {
        public IEnumerable<PaymentRefund> PaymentRefundDetails { get; set; }
        public int PaymentRefundCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
