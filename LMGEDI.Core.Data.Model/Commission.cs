using System;

namespace LMGEDI.Core.Data.Model
{
    public class Commission
    {
        public int CommissionID { get; set; }
        public int LienCommissionID { get; set; }
        public string LienCName { get; set; }
        public int LienCClientID { get; set; }
        public DateTime? LienCStartDate { get; set; }
        public DateTime? LienCEndDate { get; set; }
        public decimal? LienCPrecentage { get; set; }
        public bool? IsPaymentRecevied { get; set; }
        public string LienClientName { get; set; }
        public decimal? CAmountDue { get; set; }
        public DateTime? CDueDate { get; set; }
        public decimal? CTotalInvoicedAmount { get; set; }
        public int InvoiceDateQuater { get; set; }
        public int CDueYear { get; set; }
    }
}
