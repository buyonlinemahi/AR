using System;

namespace LMGEDI.Core.Data.Model
{
    public class OCRPayment
    {
        public int OCRPaymentId { get; set; }
        public int OCRId { get; set; }
        public int PaymentId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }

    public class OCRPaymentDetails
    {
        public int FileID { get; set; }
        public string FilesName { get; set; }
        public string ClaimNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int InvoiceID { get; set; }
        public int PaymentId{ get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceived { get; set; }
        public string CheckNumber { get; set; }
        public int OCRPaymentId { get; set; }
    }

    public class OCRPaymentSave
    {
        public int FileID { get; set; }
        public int OCRId { get; set; }
        public int CreatedBy { get; set; }
        public int InvoiceID { get; set; }
        public int PaymentId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceived { get; set; }
        public string CheckNumber { get; set; }
        public DateTime CheckDate { get; set; }
        public string CheckUploadName { get; set; }
    }
}
