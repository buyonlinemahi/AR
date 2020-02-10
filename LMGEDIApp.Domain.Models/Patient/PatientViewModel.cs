using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMGEDIApp.Domain.Models.Patient
{
    public class PatientViewModel
    {
        public PatientSearch patientSearch { get; set; }
        public IEnumerable<PatientSearchResult> PatientSearchResult { get; set; }
        public int PatientCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
