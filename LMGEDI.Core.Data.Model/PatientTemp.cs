
namespace LMGEDI.Core.Data.Model
{
    public class PatientTemp
    {

        public int PatientID { get; set; }
        public string PatientAccount { get; set; }
        public string PatientFirst { get; set; }
        public string PatientLast { get; set; }
        public string PatientGender { get; set; }
        public string PatientDOB { get; set; }
        public string PatientDOI { get; set; }
        public string PatientClaim { get; set; }
        public string PatientSSN { get; set; }
        public string PatientWCAB { get; set; }
        public string PatientTotal { get; set; }
        public string PatientPayments { get; set; }
        public string PatientAdjustments { get; set; }
        public string PatientBalance { get; set; }
        public string PatientEmployer { get; set; }
        public string PatientInsurance { get; set; }
        public bool IsDuplicate { get; set; }


    }
}
