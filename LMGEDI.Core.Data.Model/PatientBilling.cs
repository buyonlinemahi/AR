using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDI.Core.Data.Model
{
    public class PatientBilling
    {

        public int BillingID { get; set; }
        public int PatientID { get; set; }
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
    }
}
