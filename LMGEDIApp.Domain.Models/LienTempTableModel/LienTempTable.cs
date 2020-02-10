using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.LienTempTableModel
{
    public class LienTempTable
    {
        public int UploadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Claim { get; set; }
        public string Insurer { get; set; }
        public string InsurerBranch { get; set; }
        public string Employer { get; set; }
        public string Adjuster { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceAmount { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceSent { get; set; }
        public string BillingWeek { get; set; }
        public string InvoiceDept { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentReceived { get; set; }
        public string CheckNumber { get; set; }
        public bool LienDataValidate { get; set; }
        public string Reason { get; set; }
        public bool IsFirstNameUpdate { get; set; }
        public bool IsLastNameUpdate { get; set; }
        public bool IsInsurerUpdate { get; set; }
        public bool IsInsurerBranchUpdate { get; set; }
        public bool IsEmployerUpdate { get; set; }
        public bool IsAdjusterUpdate { get; set; }
    }
}
