
namespace LMGEDIApp.Domain.Models.PatientModel
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientAccount { get; set; }
        public string PatientFullName { get; set; }
        public string PatientDOB { get; set; }
        public string PatientClaim { get; set; }
        public string PatientSSN { get; set; }
        public string PatientWCAB { get; set; }
        public decimal PatientTotal { get; set; }
        public decimal PatientPayments { get; set; }
        public decimal PatientAdjustments { get; set; }
        public decimal PatientBalance { get; set; }
        public string PatientEmployer { get; set; }
        public string PatientInsurance { get; set; }
        public string PatientGender { get; set; }
        public string PatientFirst { get; set; }
        public string PatientLast { get; set; } 
    }
}
