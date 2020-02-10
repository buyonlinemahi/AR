using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;
using LMGEDI.Core.Data;
using LMGEDI.Core.BL.Model;
using System.Collections.Generic;

namespace LMGEDI.Core.BL.Implementation
{
    public class PatientTempImpl : IPatientTemp
    {
        private readonly IPatientTempRepository _tempExcelDataRepository;
        public PatientTempImpl(IPatientTempRepository tempExcelDataRepository)
        {
            _tempExcelDataRepository = tempExcelDataRepository;
        }

        public IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatient()
        {
            return _tempExcelDataRepository.GetAllColumnNameFromTempPatient().Select(tempExcelData => new BLModel.TempExcelDataColumns().InjectFrom(tempExcelData)).Cast<LMGEDI.Core.BL.Model.TempExcelDataColumns>().ToList();          
                
        }
        public int DuplicatePatientTempDataReplace()
        {
            return _tempExcelDataRepository.DuplicatePatientTempDataReplace();
        }

    }
}
