using LMGEDIApp.Domain.Models.Patient;

namespace LMGEDIApp.Domain.Models.PatientModel
{
    public class PatientDetailViewModel
    {
        public Patient patient { get; set; }
        public PatientHistory patientHistory { get; set; }
    }
}
