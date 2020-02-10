
using System;
namespace LMGEDIApp.Domain.Models.Patient
{
    public class PatientHistory
    {
        public int PatientID { get; set; }
        public string PatientAccountHistory { get; set; }
        public DateTime? PatientDOBHistory { get; set; }
        public string PatientClaimHistory { get; set; }
        public string PatientSSNHistory { get; set; }
        public string PatientWCABHistory { get; set; }
        public string PatientEmployerHistory { get; set; }
        public string PatientInsuranceHistory { get; set; }
        public string PatientGenderHistory { get; set; }
        public string PatientFirstHistory { get; set; }
        public string PatientLastHistory { get; set; }
        public string Description { get; set; }
    }
}
