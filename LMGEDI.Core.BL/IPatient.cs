using LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL
{
    public interface IPatient
    {
        BLModel.Paged.PatientDetail GetAllPatient(int skip, int take);
        BLModel.Paged.PatientDetail GetAllPatientByName(string FirstName, int skip, int take);
        BLModel.Paged.PatientDetail GetAllPatientByLastName(string LastName, int skip, int take);
        int UpdatePatient(Patient patient);
        BLModel.Patient GetPatientByID(int PatientID);
        int AddPatient(Patient patient);
    }
}
