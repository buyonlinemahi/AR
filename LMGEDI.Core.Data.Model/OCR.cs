using System;

namespace LMGEDI.Core.Data.Model
{
    public class OCR
    {
        public int OcrId { get; set; }
        public string OcrFileName { get; set; }
        public bool? IsOCRPaymentRecevied { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }


    public class OCRFileInvoices
    {
        public int FileID { get; set; }
        public string FilesName { get; set; }
        public string ClaimNumber { get; set; }
        public string InsurerBranchName { get; set; }
        public string InvoiceNumber { get; set; }
        public int InvoiceID { get; set; }
        public string EmployerName { get; set; }
        public string InsurerName { get; set; }
        public decimal OutstandingBalance { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
