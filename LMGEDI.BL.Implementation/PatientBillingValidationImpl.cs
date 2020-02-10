using LMGEDI.Core.Data;
using System;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMGEDI.Core.BL.Model.Paged;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;
using System.Threading.Tasks;
using LMGEDI.Core.BL.Model;

namespace LMGEDI.Core.BL.Implementation
{
    public class PatientBillingValidationImpl : IPatientBillingValidation
    {
        public readonly IPatientBillingValidationRepository _patientBillingValidationRepository;
        public PatientBillingValidationImpl(IPatientBillingValidationRepository patientBillingValidationRepository)
        {
            _patientBillingValidationRepository = patientBillingValidationRepository;
        }

        public BLModel.Paged.PatientBillingDetail GetAllFromPatientBillingValidation(int skip, int take)
        {
            return new BLModel.Paged.PatientBillingDetail
            {
                PatientBillingValidationDetails = _patientBillingValidationRepository.GetAllFromPatientBillingValidation(skip, take).Select(patientBillingValidationResult => new BLModel.PatientBillingValidation().InjectFrom(patientBillingValidationResult)).Cast<LMGEDI.Core.BL.Model.PatientBillingValidation>().ToList(),
                TotalCount = _patientBillingValidationRepository.GetAllFromPatientBillingValidationCount()
            };
        }

        public int UpdateValidateDataFromBillingValidation(DLModel.PatientBillingValidation patientBillingValidation)
        {
            return _patientBillingValidationRepository.UpdateValidateDataFromBillingValidation(patientBillingValidation);
        }



    }
}
