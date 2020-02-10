using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.ValidateExcelData
{
    public class PatientBillingResult
    {
        public int BillingID { get; set; }
        public string PatientFName { get; set; }
        public string PatientLName { get; set; }
        public string PatientSSN { get; set; }
        public string BillingInsurance { get; set; }
        public string UpdateOption { get; set; }
    }
}
