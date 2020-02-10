using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.ValidateExcelData
{
    public class PatientBillingValidationViewModel
    {
        public IEnumerable<PatientBillingResult> PatientBillingValidationResult { get; set; }
        public int PatientBillingCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
       
    }
}
