using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.ValidateExcelData
{
    public class PatientBillingValidation
    {
        public int BillingID { get; set; }
        public string PatientID { get; set; }
        public string PatientFName { get; set; }
        public string PatientLName { get; set; }
        public string PatientSSN { get; set; }
        public string BillingDOS { get; set; }
        public string BillingCode { get; set; }
        public string BillingDesc { get; set; }
        public string BillingBilledAmount { get; set; }
        public string BillingUnits { get; set; }
        public string BillingBilledDate { get; set; }
        public string BillingInsurance { get; set; }
        public string BillingAdjustment { get; set; }
        public string BillingPaymentDate { get; set; }
        public string BillingPayment { get; set; }
        public string BillingEmployer { get; set; }
        public string BillingInsurerPatientId { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
