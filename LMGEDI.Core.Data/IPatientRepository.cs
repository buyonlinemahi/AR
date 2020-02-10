using Core.Base.Data;
using LMGEDI.Core.Data.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.Data
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        IEnumerable<Patient> GetAllPatient(int skip, int take);
        IEnumerable<Patient> GetAllPatientByName(string FirstName, int skip, int take);
        IEnumerable<Patient> GetAllPatientByLastName(string LastName, int skip, int take);
        Patient GetPatientByID(int PatientID);
        int GetAllPatientCount();
        int GetAllPatientByNameCount(string FirstName);
        int GetAllPatientByLastNameCount(string LastName);
        int UpdatePatient(Patient patient);
    }
}
