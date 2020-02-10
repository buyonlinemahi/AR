using LMGEDIApp.Domain.Models.InvoiceAdjustmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LMGEDIApp.Domain.Models.InvoiceModel
{
    public class Invoice
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
        public HttpPostedFileBase UploadFile { get; set; }
    }
}
