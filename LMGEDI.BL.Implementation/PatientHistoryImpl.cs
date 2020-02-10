using Omu.ValueInjecter;
using DLModel = LMGEDI.Core.Data.Model;
using LMGEDI.Core.Data;

namespace LMGEDI.Core.BL.Implementation
{
    public class PatientHistoryImpl : IPatientHistory
    {
        private readonly IPatientHistoryRepository _patientHistoryRepository;
        public PatientHistoryImpl(IPatientHistoryRepository patientHistoryRepository)
        {
            _patientHistoryRepository = patientHistoryRepository;
        }
        public int InsertPatientUpdateHistory(DLModel.PatientHistory patientHistory)
        {
         return   _patientHistoryRepository.Add((DLModel.PatientHistory)new DLModel.PatientHistory().InjectFrom(patientHistory)).PatientHistoryID;
        }
        
    }
}
