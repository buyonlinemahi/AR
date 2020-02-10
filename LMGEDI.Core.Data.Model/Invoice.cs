using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMGEDI.Core.Data.Model
{
    public class Invoice
    {
        public int InvoiceID  { get; set; }
        public string InvoiceNumber  { get; set; }
        public decimal InvoiceAmt  { get; set; }
        public decimal? InvoiceICAmt { get; set; }
        public DateTime InvoiceDate  { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public DateTime? InvoiceSent { get; set; }
        public DateTime? BillingWeek { get; set; }
        public int? DepartmentId { get; set; }
        public int FileID { get; set; }
        public decimal InvoiceBalanceAmt { get; set; }
        public string InvoiceFileName { get; set; }
        //[ForeignKey("DepartmentId")]
        //public Department department { get; set; }
        //[ForeignKey("FileID")]
        //public File file { get; set; }
        
    }
    
    public class InvoiceDetail
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmt { get; set; }
        public decimal? InvoiceICAmt { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public DateTime? InvoiceSent { get; set; }
        public DateTime? BillingWeek { get; set; }
        public int? DepartmentId { get; set; }
        public int FileID { get; set; }
        public decimal? InvoiceBalance { get; set; }
        public decimal InvoiceBalanceAmt { get; set; }
        public decimal AdjustmentAmt { get; set; }
        public string AdjustmentNotes { get; set; }
        public string InvoiceFileName { get; set; }
    }

    public class InvoiceICDetail
    {
        public int FileID { get; set; }
        public int InvoiceID { get; set; }
        public string FileFullName { get; set; }
        public string ClaimNumber { get; set; }        
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmt { get; set; }
        public decimal? InvoiceICAmt { get; set; }        
        public int? DepartmentId { get; set; }    
    }
}
