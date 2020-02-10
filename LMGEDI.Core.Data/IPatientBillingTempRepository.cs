using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Base.Data;
using LMGEDI.Core.Data.Model;

namespace LMGEDI.Core.Data
{
    public interface IPatientBillingTempRepository : IBaseRepository<PatientBillingTemp>
    {
        IEnumerable<TempExcelDataColumns> GetAllColumnNameFromTempPatientBilling();
        int DuplicatePatientBillingTempDataReplace();
    }
}
