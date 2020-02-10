using System.Linq;
using Omu.ValueInjecter;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;
using LMGEDI.Core.Data;
using LMGEDI.Core.BL.Model;
using System.Collections.Generic;
namespace LMGEDI.Core.BL.Implementation
{
    public class PatientBillingTempImpl : IPatientBillingTemp
    {
        private readonly IPatientBillingTempRepository _patientBillingTempRepository;
        public PatientBillingTempImpl(IPatientBillingTempRepository patientBillingTempRepository)
        {
            _patientBillingTempRepository = patientBillingTempRepository;
        }

        public IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatientBilling()
        {
            return _patientBillingTempRepository.GetAllColumnNameFromTempPatientBilling().Select(tempExcelData => new BLModel.TempExcelDataColumns().InjectFrom(tempExcelData)).Cast<LMGEDI.Core.BL.Model.TempExcelDataColumns>().ToList(); 
        }

        public int DuplicatePatientBillingTempDataReplace()
        {
            return _patientBillingTempRepository.DuplicatePatientBillingTempDataReplace();
        }

    }
}
