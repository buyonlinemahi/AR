using System;
namespace LMGEDI.Core.BL.Model
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int FileId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceICAmt { get; set; }
        public decimal InvoiceAmt { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceSent { get; set; }
        public DateTime? BillingWeek { get; set; }
        public int DepartmentId { get; set; }
        public decimal InvoiceBalanceAmt { get; set; }
    }
}
