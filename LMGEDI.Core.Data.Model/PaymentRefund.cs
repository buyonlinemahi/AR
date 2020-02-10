using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data.Model
{
    public class PaymentRefund
    {
        public int PaymentRefundID  { get; set; }
        public int InvoiceId  { get; set; }
        public decimal? RefundAmount  { get; set; }
        public DateTime? RefundReceived  { get; set; }
        public string CheckNumber  { get; set; }
        public string CheckUploadName { get; set; }

    }
}
