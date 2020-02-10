using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.Patient
{
    public class PatientSearchResult
    {
        public int PatientID { get; set; }
        public string PatientFirst { get; set; }
        public string PatientLast { get; set; }
        public string PatientClaim { get; set; }
        public string PatientInsurance { get; set; }
    }
}
