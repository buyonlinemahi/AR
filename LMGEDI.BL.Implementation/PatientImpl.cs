using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;
using LMGEDI.Core.Data;

namespace LMGEDI.Core.BL.Implementation
{
    public class PatientImpl : IPatient
    {
        private readonly IPatientRepository _patientRepository;
        public PatientImpl(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public BLModel.Paged.PatientDetail GetAllPatient(int skip, int take)
        {
            return new BLModel.Paged.PatientDetail
            {
                PatientDetails = _patientRepository.GetAllPatient(skip, take).Select(patientSearchResult => new BLModel.Patient().InjectFrom(patientSearchResult)).Cast<LMGEDI.Core.BL.Model.Patient>().ToList(),
                TotalCount = _patientRepository.GetAllPatientCount()
            };
        }

        public BLModel.Paged.PatientDetail GetAllPatientByName(string FirstName, int skip, int take)
        {
            return new BLModel.Paged.PatientDetail
            {
                PatientDetails = _patientRepository.GetAllPatientByName(FirstName,skip, take).Select(patientSearchResult => new BLModel.Patient().InjectFrom(patientSearchResult)).Cast<LMGEDI.Core.BL.Model.Patient>().ToList(),
                TotalCount = _patientRepository.GetAllPatientByNameCount(FirstName)
            };
        }
        public BLModel.Paged.PatientDetail GetAllPatientByLastName(string LastName, int skip, int take)
        {
            return new BLModel.Paged.PatientDetail
            {
                PatientDetails = _patientRepository.GetAllPatientByLastName(LastName,skip, take).Select(patientSearchResult => new BLModel.Patient().InjectFrom(patientSearchResult)).Cast<LMGEDI.Core.BL.Model.Patient>().ToList(),
                TotalCount = _patientRepository.GetAllPatientByLastNameCount(LastName)
            };
        }
        public int UpdatePatient(DLModel.Patient patient)
        {
            return _patientRepository.UpdatePatient(patient);
        }
        public BLModel.Patient GetPatientByID(int patientID)
        {
            return (BLModel.Patient)new BLModel.Patient().InjectFrom(_patientRepository.GetPatientByID(patientID));
        }
        public int AddPatient(DLModel.Patient patient)
        {
            return _patientRepository.Add((DLModel.Patient)new DLModel.Patient().InjectFrom(patient)).PatientID;
        }
        

    }
}
