using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data.Model
{
    public class ContractorTempTable
    {
        public int UploadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceAmount { get; set; }
        public string InvoiceDate { get; set; }
        public string LienInvoiceNumber { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentSent { get; set; }
        public string CheckNumber { get; set; }
        public string InvoiceDept { get; set; }
        public int PendingUploadId { get; set; }

    }
}
