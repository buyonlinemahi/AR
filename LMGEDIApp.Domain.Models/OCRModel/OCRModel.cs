using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.OCRModel
{
    public class OCRModel
    {
        public int OcrId { get; set; }
        public string OcrFileName { get; set; }
        public string OcrFilePath { get; set; }
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

    public class OCRPaymentDetails
    {
        public int FileID { get; set; }
        public string FilesName { get; set; }
        public string ClaimNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int InvoiceID { get; set; }
        public int PaymentId { get; set; }
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
        public string FilesName { get; set; }
        public string ClaimNumber { get; set; }
        public string InvoiceNumber { get; set; }
    }

    public class OCRPayment
    {
        public int OCRPaymentId { get; set; }
        public int OCRId { get; set; }
        public int PaymentId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
