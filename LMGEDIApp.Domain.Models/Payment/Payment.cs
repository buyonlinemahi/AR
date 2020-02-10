using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LMGEDIApp.Domain.Models.PaymentModel
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceived { get; set; }
        public string CheckNumber { get; set; }
        public HttpPostedFileBase CheckUploadName1 { get; set; }
        public string CheckUploadName { get; set; }
        public string CheckDownloadPath { get; set; }
        public bool flag { get; set; }
        public int FileID { get; set; }
        public bool IsPaymentGreater { get; set; }
        public int? PendingUploadId { get; set; }

    }
}
