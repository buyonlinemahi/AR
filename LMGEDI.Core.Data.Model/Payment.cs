using System;

namespace LMGEDI.Core.Data.Model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceived { get; set; }
        public string CheckNumber { get; set; }
        public string CheckUploadName { get; set; }
        public int? PendingUploadId { get; set; }
    }
}
