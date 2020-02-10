using LMGEDI.Core.Data.Model;
using BLModel = LMGEDI.Core.BL.Model;
using DLModel = LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.BL
{
    public interface IPatientBillingValidation
    {
        BLModel.Paged.PatientBillingDetail GetAllFromPatientBillingValidation(int skip, int take);
        int UpdateValidateDataFromBillingValidation(DLModel.PatientBillingValidation patientBillingValidation);
    }
}
