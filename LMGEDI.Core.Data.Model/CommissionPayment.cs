using System;

namespace LMGEDI.Core.Data.Model
{
    public class CommissionPayment
    {
        public int CommissionPaymentID { get; set; }
        public int CommissionID { get; set; }
        
        public decimal? CPPaymentPaidAmount { get; set; }
        public string CPCheckNumber { get; set; }
        public DateTime? CPCheckSentOn { get; set; }
        public int InvoiceDateQuarter { get; set; }
        public bool? IsPaymentRecevied { get; set; }
    }
}
